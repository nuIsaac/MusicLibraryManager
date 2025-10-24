const API = "/api";
const tbody = document.querySelector("#songTable tbody");
const audio = document.getElementById("audio");
const now = document.getElementById("now");
const form = document.getElementById("uploadForm");

async function listSongs(){
  const res = await fetch(`${API}/songs`);
  const songs = await res.json();
  tbody.innerHTML = "";
  for(const s of songs){
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td>${s.title}</td>
      <td><button data-play="${s.filePath}" data-title="${s.title}">Play</button></td>
      <td><button data-del="${s.id}">Delete</button></td>`;
    tbody.appendChild(tr);
  }
}

form.addEventListener("submit", async (e)=>{
  e.preventDefault();
  const fd = new FormData(form);
  const res = await fetch(`${API}/songs/upload`, { method:"POST", body: fd });
  if(!res.ok){ alert(await res.text()); return; }
  form.reset();
  await listSongs();
});

tbody.addEventListener("click", async (e)=>{
  const play = e.target.closest("button[data-play]");
  if(play){
    const src = play.getAttribute("data-play");
    const title = play.getAttribute("data-title");
    audio.src = src; audio.play();
    now.textContent = `Now Playing: ${title}`;
    return;
  }
  const del = e.target.closest("button[data-del]");
  if(del){
    const id = del.getAttribute("data-del");
    const res = await fetch(`${API}/songs/${id}`, { method:"DELETE" });
    if(res.ok) listSongs();
  }
});

listSongs();
