# 🎧 Music Library Manager  
*A full-stack web app built with ASP.NET Core and Entity Framework Core, evolving into a cloud-native music storage platform.*

---

## 🌟 Vision

The **Music Library Manager** started as a simple local web app to upload, organize, and play songs — but the end goal is much bigger:

> 🧱 To design a **scalable, cloud-ready music library service** using modern Microsoft and AWS technologies.

The project will eventually support distributed storage, real-time analytics, and user-specific libraries — mirroring the type of full-stack systems used in real production environments.

---

## 🚀 Current Phase (Phase 1: Local App Foundation)

✅ Built a full local web app stack using:
- **C# / ASP.NET Core 8** for the backend API  
- **Entity Framework Core** for database access  
- **SQLite** for lightweight persistence  
- **HTML / CSS / JavaScript** for the frontend  

### Core Features
- Upload audio files (.mp3, .wav, .ogg, etc.)
- Store metadata (title, path, upload date) in SQLite
- Stream music directly in the browser
- Delete songs from both database and filesystem
- Swagger UI auto-documentation for API testing

### Architecture
Frontend (HTML/JS)
↓
ASP.NET Core Web API (C#)
↓
Entity Framework Core
↓
SQLite database + local uploads


---

## 🔭 Next Phases (In Progress)

### **Phase 2 — Docker & Deployment**
- Containerize the app with Docker  
- Add persistent volumes for uploads and database  
- Configure multi-stage builds for smaller image sizes  

### **Phase 3 — Cloud Integration (AWS)**
- Replace SQLite with **PostgreSQL (Amazon RDS)**  
- Move audio files to **Amazon S3**  
- Add **AWS Lambda** function triggers for metadata extraction  
- Integrate **CloudWatch logging** and **IAM-based access**

### **Phase 4 — Frontend Expansion**
- React or Blazor UI for dynamic playlists  
- Authentication (JWT + ASP.NET Identity)  
- User-specific song libraries & favorites  

### **Phase 5 — Analytics & Recommendations**
- Add server-side audio analysis (BPM, length, genre detection)  
- Basic machine-learning recommendation engine  

---

## 🧠 Tech Stack

| Layer | Tools |
|-------|-------|
| Backend | ASP.NET Core 8, C#, EF Core |
| Database | SQLite → PostgreSQL (future) |
| Frontend | HTML, CSS, JavaScript |
| Infrastructure | Docker, AWS (future) |
| Dev Tools | VS Code, Git, PowerShell |

---

## 🖥️ Run Locally

```bash
# Clone repo
git clone https://github.com/<yourusername>/MusicLibraryManager.git
cd MusicLibraryManager

# Restore and build
dotnet restore
dotnet ef database update
dotnet run
Visit http://localhost:5239


