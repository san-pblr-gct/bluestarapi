Chat Application using Socket.io and Mongo db written in ES6 way.

Key points while setting up:
If you are using mongo first time, please do the below steps.
1. install mongo from their website.
2. Create a folder /data/db in C: (Mongo works only in this folder)
3. Go to mongo bin located inside Program files (C:\Program Files\MongoDB\Server\3.4\bin) and open command prompt and run "mongod"
4. The above step will start mongodb
5. Open separate command window inside mongo bin and run " mongo"


Some of the commands for mongo which you need to see the data.
1. show dbs (shows all databases)
2. db (shows which database you are in)
3. use xxx (xxx refers to the database you want to navigate to)
4. show collections (show collections inside the database)
5. db.xxx.find() (To show the data inside the collection, replace xxx with the collections)

Mongo cheatsheet for future references
https://gist.github.com/aponxi/4380516


To run the application
1. install npm
2. Also install nodemon globally "npm install nodemon -g"
3. run npm start in cmd
4. Browse the html page in firefox or host it in IIS or other web server using parcel/webpack/express. Running file directly in chrome will mess up with connecting socket and it won't work

Inspired from Brad traversy tutorial.

Future plans:
1. Show list of connected users.
2. Send screenshot using html2canvas across page.