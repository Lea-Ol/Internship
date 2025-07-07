const API_BASE = "http://localhost:5154/api";

function getToken() {
  return localStorage.getItem("token");
}
function getUserEmail() {
  return localStorage.getItem("userEmail");
}
function getUserRole() {
  return localStorage.getItem("userRole");
}
function getUserId() {
  return localStorage.getItem("userId");
}
function logout() {
  localStorage.removeItem("token");
  localStorage.removeItem("userEmail");
  localStorage.removeItem("userRole");
  localStorage.removeItem("userId");
  location.hash = "#login";
}
async function apiFetch(endpoint, options = {}) {
  const headers = options.headers || {};
  if (getToken()) headers["Authorization"] = "Bearer " + getToken();
  if (!options.noContentType) headers["Content-Type"] = "application/json";
  const res = await fetch(API_BASE + endpoint, { ...options, headers });
  if (!res.ok) throw new Error(await res.text());
  if (res.status === 204) return null;
  return res.json();
}

function renderLogin() {
  document.getElementById('app').innerHTML = `
    <div class="container">
      <h2>Login</h2>
      <form id="loginForm">
        <input type="text" id="loginEmail" placeholder="Email" required>
        <input type="password" id="loginPassword" placeholder="Password" required>
        <button type="submit">Login</button>
        <div class="flex-between">
          <a href="#forgot" class="link">Forgot Password?</a>
        </div>
      </form>
    </div>
  `;
  document.getElementById('loginForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    const email = document.getElementById('loginEmail').value.trim();
    const password = document.getElementById('loginPassword').value.trim();
    try {
      const data = await apiFetch("/Auth/login", {
        method: "POST",
        body: JSON.stringify({ email, password })
      });
      localStorage.setItem("token", data.token);
      localStorage.setItem("userEmail", data.email);
      localStorage.setItem("userRole", data.role);
      // Optionally fetch userId
      const user = await apiFetch(`/Users?email=${encodeURIComponent(data.email)}`);
      if (user && user.length > 0) localStorage.setItem("userId", user[0].userId);
      location.hash = "#dashboard";
    } catch (err) {
      alert("Login failed: " + err.message);
    }
  });
}

async function renderDashboard() {
  document.getElementById('app').innerHTML = `<div class="container" style="max-width:900px;"><h2>Loading...</h2></div>`;
  let meetings = [], rooms = [], notifications = [];
  try {
    meetings = await apiFetch(`/Meetings/user/${getUserId()}`);
    rooms = await apiFetch(`/Rooms`);
    notifications = await apiFetch(`/Notifications`);
  } catch (e) { alert("Failed to load dashboard data: " + e.message); }
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:900px;">
      <h2>Dashboard</h2>
      <button onclick="logout()" style="float:right;">Logout</button>
      <div class="grid" style="grid-template-columns: 2fr 1fr; gap:2rem;">
        <div>
          <h3>Upcoming Meetings</h3>
          ${meetings.map(m => `<div class="card">${new Date(m.scheduledStart).toLocaleTimeString()} - ${m.title} (${rooms.find(r => r.roomId === m.bookingId)?.roomName || 'Room' })</div>`).join('')}
          <div class="flex" style="margin-top:1.5rem;">
            <button onclick="location.hash='#book'">Schedule Meeting</button>
            <button id="joinNowBtn">Join Now</button>
            <button id="viewMinutesBtn">View Minutes</button>
          </div>
        </div>
        <div>
          <h3>Room Availability</h3>
          <div class="calendar">
            ${rooms.map(r => `<div class="badge">${r.roomName}</div>`).join('')}
          </div>
          <h3 style="margin-top:2rem;">Notifications</h3>
          ${notifications.map(n => `<div class="card">${n.title}</div>`).join('')}
        </div>
      </div>
    </div>
  `;
  document.getElementById('joinNowBtn').onclick = function() { location.hash = '#meeting'; };
  document.getElementById('viewMinutesBtn').onclick = function() { location.hash = '#review'; };
}

async function renderBooking() {
  let rooms = [];
  try { rooms = await apiFetch(`/Rooms`); } catch (e) { alert("Failed to load rooms: " + e.message); }
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:600px;">
      <h2>Book a Meeting Room</h2>
      <form id="bookingForm">
        <input type="text" id="meetingTitle" placeholder="Meeting Title" required>
        <div class="flex">
          <input type="date" id="meetingDate" required>
          <input type="time" id="meetingTime" required>
        </div>
        <input type="number" id="duration" placeholder="Duration (minutes)" required>
        <input type="text" id="attendees" placeholder="Attendees (comma separated emails)">
        <select id="roomSelect">
          ${rooms.map(r => `<option value="${r.roomId}">${r.roomName}</option>`).join('')}
        </select>
        <div class="flex">
          <button type="submit">Book Now</button>
          <button type="button" onclick="location.hash='#dashboard'">Cancel</button>
        </div>
      </form>
    </div>
  `;
  document.getElementById('bookingForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    const title = document.getElementById('meetingTitle').value.trim();
    const date = document.getElementById('meetingDate').value;
    const time = document.getElementById('meetingTime').value;
    const duration = parseInt(document.getElementById('duration').value);
    const attendees = document.getElementById('attendees').value.split(',').map(x => x.trim()).filter(Boolean);
    const roomId = parseInt(document.getElementById('roomSelect').value);
    const startTime = new Date(`${date}T${time}`);
    const endTime = new Date(startTime.getTime() + duration * 60000);
    try {
      
      const booking = await apiFetch("/Bookings", {
        method: "POST",
        body: JSON.stringify({
          roomId,
          bookedByUserId: getUserId(),
          startTime,
          endTime,
          status: 0, 
          notes: title,
          createdAt: new Date(),
          updatedAt: new Date()
        })
      });
      
      await apiFetch("/Meetings", {
        method: "POST",
        body: JSON.stringify({
          bookingId: booking.bookingId,
          title,
          agenda: "",
          description: "",
          scheduledStart: startTime,
          scheduledEnd: endTime,
          status: 0, 
          createdAt: new Date(),
          updatedAt: new Date()
        })
      });
      alert("Room booked and meeting created!");
      location.hash = "#dashboard";
    } catch (err) {
      alert("Booking failed: " + err.message);
    }
  });
}

function renderActiveMeeting() {
  
  document.getElementById('app').innerHTML = `<div class="container"><h2>Loading...</h2></div>`;
  apiFetch(`/Meetings/user/${getUserId()}`).then(meetings => {
    const meeting = meetings[0];
    if (!meeting) {
      document.getElementById('app').innerHTML = `<div class="container"><h2>No Active Meeting</h2></div>`;
      return;
    }
    document.getElementById('app').innerHTML = `
      <div class="container" style="max-width:700px;">
        <h2>Active Meeting</h2>
        <div class="card">
          <div class="flex-between">
            <div>
              <strong>${meeting.title}</strong><br>
              ${new Date(meeting.scheduledStart).toLocaleTimeString()} - ${new Date(meeting.scheduledEnd).toLocaleTimeString()}<br>
              Attendees:
              <ul style="margin:0 0 0 1.2em;padding:0;">
                <!-- TODO: Fetch attendees -->
              </ul>
            </div>
            <div style="text-align:right;">
              <button id="startMeetingBtn">Start Meeting</button>
              <button id="endMeetingBtn">End Meeting</button>
              <div style="margin-top:0.5em;"><span class="badge" id="meetingTimer">00:00:00</span></div>
            </div>
          </div>
        </div>
        <div class="flex" style="margin-bottom:1rem;">
          <button id="takeNotesBtn">Take Notes</button>
          <button>Share Screen</button>
          <button>Invite Participant</button>
          <label style="display:flex;align-items:center;gap:0.5em;">
            <input type="checkbox" id="transcriptionToggle"> Live Transcription
          </label>
          <a href="#" class="link">Join via Zoom</a>
          <a href="#" class="link">Join via Teams</a>
        </div>
      </div>
    `;
    
    let timer = 0, interval;
    const timerEl = document.getElementById('meetingTimer');
    document.getElementById('startMeetingBtn').onclick = function() {
      if (!interval) interval = setInterval(() => {
        timer++;
        const h = String(Math.floor(timer/3600)).padStart(2,'0');
        const m = String(Math.floor((timer%3600)/60)).padStart(2,'0');
        const s = String(timer%60).padStart(2,'0');
        timerEl.textContent = `${h}:${m}:${s}`;
      }, 1000);
    };
    document.getElementById('endMeetingBtn').onclick = function() {
      clearInterval(interval); interval = null;
    };
    document.getElementById('takeNotesBtn').onclick = function() {
      location.hash = '#minutes';
    };
  });
}

async function renderMinutes() {
  document.getElementById('app').innerHTML = `<div class="container"><h2>Loading...</h2></div>`;
  let meetings = [];
  try { meetings = await apiFetch(`/Meetings/user/${getUserId()}`); } catch (e) {}
  const meeting = meetings[0];
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:700px;">
      <h2>Meeting Minutes</h2>
      <form id="minutesForm">
        <label>Attendees</label>
        <input type="text" value="" id="attendeesInput">
        <label>Agenda Items</label>
        <textarea rows="2" id="agendaInput"></textarea>
        <label>Decisions/Action Items (with assignee)</label>
        <textarea rows="3" id="decisionsInput" placeholder="e.g. Carol to send report by Friday"></textarea>
        <label>Attachments</label>
        <input type="file" id="attachmentsInput" multiple>
        <div class="flex">
          <button type="button">Save Draft</button>
          <button type="submit">Finalize & Share</button>
        </div>
      </form>
    </div>
  `;
  document.getElementById('minutesForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    try {
      await apiFetch("/MeetingMinutes", {
        method: "POST",
        body: JSON.stringify({
          meetingId: meeting.meetingId,
          createdByUserId: getUserId(),
          discussionPoints: document.getElementById('agendaInput').value,
          decisionsMade: document.getElementById('decisionsInput').value,
          nextSteps: "",
          createdAt: new Date(),
          updatedAt: new Date()
        })
      });
      alert("Minutes saved!");
      location.hash = "#dashboard";
    } catch (err) {
      alert("Failed to save minutes: " + err.message);
    }
  });
}

async function renderMinutesReview() {
  document.getElementById('app').innerHTML = `<div class="container"><h2>Loading...</h2></div>`;
  let meetings = [];
  try { meetings = await apiFetch(`/Meetings/user/${getUserId()}`); } catch (e) {}
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:900px;">
      <h2>Past Meetings & Minutes</h2>
      <div class="flex-between" style="margin-bottom:1em;">
        <input type="date" style="max-width:180px;">
        <input type="text" placeholder="Search by keyword or attendee" style="max-width:300px;">
      </div>
      ${meetings.map(m => `<div class="card flex-between">
        <div>
          <strong>${m.title}</strong> (${new Date(m.scheduledStart).toLocaleTimeString()})<br>
          Action Items: <span class="badge">Pending</span>
        </div>
        <div class="flex">
          <button>Edit</button>
          <button>Export PDF</button>
          <button>Export DOC</button>
          <button>Share</button>
        </div>
      </div>`).join('')}
    </div>
  `;
}

async function renderAdminPanel() {
  document.getElementById('app').innerHTML = `<div class="container"><h2>Loading...</h2></div>`;
  let rooms = [];
  try { rooms = await apiFetch(`/Rooms`); } catch (e) {}
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:900px;">
      <h2>Admin Panel</h2>
      <h3>Room List</h3>
      ${rooms.map(r => `<div class="card flex-between"><div>${r.roomName}</div><div class="badge">${r.isActive ? 'Available' : 'Inactive'}</div></div>`).join('')}
      <h3 style="margin-top:2rem;">Add/Edit Room</h3>
      <form class="flex" style="gap:0.5em;" id="roomForm">
        <input type="text" id="roomName" placeholder="Room Name">
        <input type="number" id="roomCapacity" placeholder="Capacity" min="0">
        <input type="text" id="roomFeatures" placeholder="Equipment (mic, projector)">
        <button type="submit">Add Room</button>
      </form>
      <h3 style="margin-top:2rem;">Analytics</h3>
      <div class="card">Room usage: 75% this week</div>
      <div class="card">Most booked: Room B</div>
      <div class="card">Least used: Room C</div>
    </div>
  `;
  document.getElementById('roomForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    try {
      await apiFetch("/Rooms", {
        method: "POST",
        body: JSON.stringify({
          roomName: document.getElementById('roomName').value,
          capacity: parseInt(document.getElementById('roomCapacity').value),
          otherFeatures: document.getElementById('roomFeatures').value,
          isActive: true,
          createdAt: new Date(),
          updatedAt: new Date()
        })
      });
      alert("Room added!");
      renderAdminPanel();
    } catch (err) {
      alert("Failed to add room: " + err.message);
    }
  });
}

function router() {
  if (!getToken() && location.hash !== "#login") {
    location.hash = "#login";
    return;
  }
  switch(location.hash) {
    case '#dashboard': renderDashboard(); break;
    case '#book': renderBooking(); break;
    case '#meeting': renderActiveMeeting(); break;
    case '#minutes': renderMinutes(); break;
    case '#review': renderMinutesReview(); break;
    case '#admin': renderAdminPanel(); break;
    default: renderLogin();
  }
}

window.addEventListener('hashchange', router);
window.addEventListener('DOMContentLoaded', router);
// Expose logout globally
window.logout = logout; 