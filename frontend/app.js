function renderLogin() {
  document.getElementById('app').innerHTML = `
    <div class="container">
      <h2>Login</h2>
      <form id="loginForm">
        <input type="text" id="loginEmail" placeholder="Email or Username" required>
        <input type="password" id="loginPassword" placeholder="Password" required>
        <button type="submit">Login</button>
        <div class="flex-between">
          <a href="#forgot" class="link">Forgot Password?</a>
        </div>
      </form>
    </div>
  `;
  document.getElementById('loginForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const email = document.getElementById('loginEmail').value.trim();
    const password = document.getElementById('loginPassword').value.trim();
    if (email === 'admin' && password === 'admin') {
      location.hash = '#admin';
    } else {
      location.hash = '#dashboard';
    }
  });
}

function renderDashboard() {
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:900px;">
      <h2>Dashboard</h2>
      <div class="grid" style="grid-template-columns: 2fr 1fr; gap:2rem;">
        <div>
          <h3>Upcoming Meetings</h3>
          <div class="card">10:00 AM - Project Sync (Room A)</div>
          <div class="card">1:00 PM - Design Review (Room B)</div>
          <div class="card">3:30 PM - Client Call (Room C)</div>
          <div class="flex" style="margin-top:1.5rem;">
            <button onclick="location.hash='#book'">Schedule Meeting</button>
            <button id="joinNowBtn">Join Now</button>
            <button id="viewMinutesBtn">View Minutes</button>
          </div>
        </div>
        <div>
          <h3>Room Availability</h3>
          <div class="calendar">
            <div class="badge" style="background:#bbf7d0;color:#166534;">A</div>
            <div class="badge" style="background:#fee2e2;color:#991b1b;">B</div>
            <div class="badge" style="background:#fef9c3;color:#a16207;">C</div>
            <div class="badge">D</div>
            <div class="badge">E</div>
            <div class="badge">F</div>
            <div class="badge">G</div>
          </div>
          <h3 style="margin-top:2rem;">Notifications</h3>
          <div class="card">Meeting in 30 minutes: Project Sync</div>
          <div class="card">Pending Minutes: Design Review</div>
        </div>
      </div>
    </div>
  `;
  document.getElementById('joinNowBtn').onclick = function() {
    location.hash = '#meeting';
  };
  document.getElementById('viewMinutesBtn').onclick = function() {
    location.hash = '#review';
  };
}

function renderBooking() {
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:600px;">
      <h2>Book a Meeting Room</h2>
      <form>
        <input type="text" placeholder="Meeting Title" required>
        <div class="flex">
          <input type="date" required>
          <input type="time" required>
        </div>
        <input type="number" placeholder="Duration (minutes)" required>
        <input type="text" placeholder="Attendees (search/add)">
        <select>
          <option>Room A</option>
          <option>Room B</option>
          <option>Room C</option>
        </select>
        <div class="calendar" style="margin-bottom:1rem;">
          <div class="badge" style="background:#bbf7d0;color:#166534;">A</div>
          <div class="badge" style="background:#fee2e2;color:#991b1b;">B</div>
          <div class="badge" style="background:#fef9c3;color:#a16207;">C</div>
        </div>
        <div class="flex-between">
          <label><input type="checkbox"> Recurring meeting</label>
          <label><input type="checkbox"> Video Conferencing</label>
        </div>
        <div class="flex">
          <button type="submit">Book Now</button>
          <button type="button" onclick="location.hash='#dashboard'">Cancel</button>
        </div>
      </form>
    </div>
  `;
}

function renderActiveMeeting() {
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:700px;">
      <h2>Active Meeting</h2>
      <div class="card">
        <div class="flex-between">
          <div>
            <strong>Project Kickoff</strong><br>
            10:00 AM - 11:00 AM<br>
            Attendees:
            <ul style="margin:0 0 0 1.2em;padding:0;">
              <li>Alice</li>
              <li>Bob</li>
              <li>Carol</li>
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
  // Simple timer logic (placeholder)
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
}

function renderMinutes() {
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:700px;">
      <h2>Meeting Minutes</h2>
      <form id="minutesForm">
        <label>Attendees</label>
        <input type="text" value="Alice, Bob, Carol">
        <label>Agenda Items</label>
        <textarea rows="2">1. Project Updates\n2. Next Steps</textarea>
        <label>Decisions/Action Items (with assignee)</label>
        <textarea rows="3" placeholder="e.g. Carol to send report by Friday"></textarea>
        <label>Attachments</label>
        <input type="file" multiple>
        <div class="flex">
          <button type="button">Save Draft</button>
          <button type="submit">Finalize & Share</button>
        </div>
      </form>
    </div>
  `;
}

function renderMinutesReview() {
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:900px;">
      <h2>Past Meetings & Minutes</h2>
      <div class="flex-between" style="margin-bottom:1em;">
        <input type="date" style="max-width:180px;">
        <input type="text" placeholder="Search by keyword or attendee" style="max-width:300px;">
      </div>
      <div class="card flex-between">
        <div>
          <strong>Project Kickoff</strong> (10:00 AM)<br>
          Action Items: <span class="badge" style="background:#fef9c3;color:#a16207;">Pending</span>
        </div>
        <div class="flex">
          <button>Edit</button>
          <button>Export PDF</button>
          <button>Export DOC</button>
          <button>Share</button>
        </div>
      </div>
      <div class="card flex-between">
        <div>
          <strong>Design Review</strong> (1:00 PM)<br>
          Action Items: <span class="badge" style="background:#bbf7d0;color:#166534;">Completed</span>
        </div>
        <div class="flex">
          <button>Edit</button>
          <button>Export PDF</button>
          <button>Export DOC</button>
          <button>Share</button>
        </div>
      </div>
    </div>
  `;
}

function renderAdminPanel() {
  document.getElementById('app').innerHTML = `
    <div class="container" style="max-width:900px;">
      <h2>Admin Panel</h2>
      
      <h3>Room List</h3>
      <div class="card flex-between">
        <div>Room A</div>
        <div class="badge" style="background:#bbf7d0;color:#166534;">Available</div>
      </div>
      <div class="card flex-between">
        <div>Room B</div>
        <div class="badge" style="background:#fee2e2;color:#991b1b;">Booked</div>
      </div>
      <div class="card flex-between">
        <div>Room C</div>
        <div class="badge" style="background:#fef9c3;color:#a16207;">Maintenance</div>
      </div>

      <h3 style="margin-top:2rem;">Add/Edit Room</h3>
      <form class="flex" style="gap:0.5em;">
        <input type="text" placeholder="Room Name">
        <input type="number" placeholder="Capacity" min="0" step="1" value="0">
        <input type="text" placeholder="Equipment (mic, projector)">
        <button>Add Room</button>
      </form>

      <h3 style="margin-top:2rem;">Analytics</h3>
      <div class="card">Room usage: 75% this week</div>
      <div class="card">Most booked: Room B</div>
      <div class="card">Least used: Room C</div>
    </div>
  `;
}


function router() {
  switch(location.hash) {
    case '#dashboard':
      renderDashboard(); break;
    case '#book':
      renderBooking(); break;
    case '#meeting':
      renderActiveMeeting(); break;
    case '#minutes':
      renderMinutes(); break;
    case '#review':
      renderMinutesReview(); break;
    case '#admin':
      renderAdminPanel(); break;
    default:
      renderLogin();
  }
}

window.addEventListener('hashchange', router);
window.addEventListener('DOMContentLoaded', router);