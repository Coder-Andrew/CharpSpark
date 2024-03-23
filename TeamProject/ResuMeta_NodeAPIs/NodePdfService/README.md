### How to set up ###
I use the CLI for most everything, so I don't know the translation to Visual Studio -- sorry Andrew XD
- first ensure you have "npm" and "node" installed on your machine
  --to check run `which npm` & `which node` (for linux/OS X), if they are installed then you should see a path outputted from those commands
- open a CLI & `cd` into "ResuMeta_NodeAPI/NodePdf"
- from here, package.json includes the packages used in this project, to set up the packages run `npm install`
- you should now see a "node_modules" folder in the project, this folder contains the packages
- now, run `node .` to start the server, it will start "index.js" which will listen for requests
- you're now ready to try exporting a resume from the .NET server, just remember to set the NodeUrl variable:
`dotnet user-secrets set NodeUrl "http://localhost:8080"` ~ for local development