RaceDay – Event Management System

System Overview
RaceDay is a web-based event management platform designed for South Africa’s road running, walking, and cycling communities. It modernizes paper-based registration by enabling organizers to create and manage events, categories, and results, while participants can browse events, register, and track their performance history.
The system is built progressively across three parts:
•	Part 1: Planning documents (ERD, API plan, SQL script).
•	Part 2: Implementation with CI/CD workflows.
•	Part 3: Final containerised, cloud-aware deployment.
Roles
•	Organizer
o	Create, update, and delete events.
o	Define categories (age/distance).
o	View enrolments.
o	Record race results.
•	Participant
o	Register and log in.
o	View and update profile.
o	Browse and enroll in events.
o	View personal and public results.
Repository Structure
Code
/docs
  POE_ERD.png
  POE_API_ENDPOINT_PLAN.md 
  RaceDay_Script.sql 


CI/CD Workflow
•	Configured with GitHub Actions to validate repository structure.
•	Ensures /docs folder exists and contains required files.
•	Includes automated checks for commit history and file presence.
Screenshot of successful green build: (PDF version of README will be included) 
Video Presentation
Unlisted YouTube link: (Video Link to be updated)
The video walkthrough covers:
•	ERD design decisions.
•	API endpoint plan choices.
•	SQL script execution live in SSMS.
•	CI/CD validation demonstration.
