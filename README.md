# AutoProxy
A drop-in RESTful API proxy for Autotask Web Services API using ASP.NET Core.

Add your AT integration code, and AT API un/pw to appsettings-defaults.json then save it as appsettings.json in your executable directory.

This project is at best a proof of concept right now, though I am actively improving upon it in my free time.

Currently supports HTTP GET for Accounts only.

https://localhost:5000/api/Accounts/0<br />
https://localhost:5000/api/Accounts?filter=AccountName%20contains%20Acme
