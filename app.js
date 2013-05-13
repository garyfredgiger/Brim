var express = require("express");
var app = express();
var fs = require('fs');
var mongoExpressAuth = require('mongo-express-auth');
var request = require('request');







//===========================
//  init
//===========================

mongoExpressAuth.init({
    mongo: { 
        dbName: 'myApp',
        collectionName: 'accounts'
    }
}, function(){
    console.log('mongo ready!');
    app.listen(8889);
});

app.use(express.bodyParser({uploadDir:'./static/uploads'}));
app.use(express.cookieParser());
app.use(express.session({ secret: 'this is supposed to be secret, change it' }));

//===========================
//  routes
//===========================

app.get('/', function(req, res){
    mongoExpressAuth.checkLogin(req, res, function(err){
        if (err)
            res.sendfile('static/login.html');
        else
            res.sendfile('static/index.html');
    });
});

app.get('/me', function(req, res){
    mongoExpressAuth.checkLogin(req, res, function(err){
        if (err)
            res.send(err);
        else {
            mongoExpressAuth.getAccount(req, function(err, result){
                if (err)
                    res.send(err);
                else 
                    res.send(result); // NOTE: direct access to the database is a bad idea in a real app
            });
        }
    });
});





app.post('/login', function(req, res){
    mongoExpressAuth.login(req, res, function(err){
        if (err)
            res.send(err); 
        else
            res.send('ok');
    });
});
 
app.post('/logout', function(req, res){
    mongoExpressAuth.logout(req, res);
    res.send('ok');
});




app.post('/register', function(req, res){ 
    mongoExpressAuth.register(req, function(err){
        if (err)
            res.send(err);
        else
            res.send('ok');
    });
});


//=================================================================
var child_process = require('child_process');
var justUploaded = "";
var converted = "";
var outPutFile = "static/foo.txt";
var filterUsed = 3;

app.post('/convert', function(req,res){
    //console.log('recieved convert request');
    justUploaded = req.body.justUploaded;
    child_process.exec('python filter.py static/'+justUploaded+' '+outPutFile+' '+filterUsed,function(error,stdout,stderr){
        if(error){
            throw error;
        }
        converted = stdout.replace('static/','');
        console.log('converted: '+ converted);
        res.send({
            success: true
        });
    });
    console.log(converted);

    
});

var download = function(uri, filename){
  request.head(uri, function(err, res, body){
    if(err){
        console.log(err);
    }
    else if ((res.headers['content-type']).indexOf('image')== -1){
        console.log('Not Image URL!');
    }
    else{
        console.log('content-type:', res.headers['content-type']);
        console.log('content-length:', res.headers['content-length']);

        request(uri).pipe(fs.createWriteStream(filename));
    }
  });
};


app.post('/onlineImage', function(req, res){
    var urlText = req.body.urlText;
    var outPutFile = 'static/uploads/trial.png';
    download(urlText,outPutFile);
    justUploaded = 'uploads/trial.png';
    console.log("jkllll")
   // res.send(justUploaded);
    res.redirect("genImage1.html");
});

app.get('/foo.txt', function(req, res){
  var file = __dirname + '/static/foo.txt';
  res.download(file); // Set disposition and send it.
});

app.post('/upload', function(req, res) { 
    console.log(req.files['thumbnail']);
    var type = req.files['thumbnail'].type;
    console.log(type);
    console.log(type !== "image/jpeg")
    if (type !== "image/jpeg" && type !== "image/png" && type !== "image/gif"){
        console.log("notanImage");
        res.redirect('notanImage.html');        
    }
    else{
    // get the name of the uploaded file and intended name
    var fileName = req.files['thumbnail'].path;
    var newName = req.files['thumbnail'].name;
    var srcimg = "static/uploads/"+newName;
    justUploaded = "uploads/"+newName;

    fs.renameSync(fileName, srcimg);
    if (mongoExpressAuth.isLoggedIn(req)){
        mongoExpressAuth.updateUploadImage(req, justUploaded, function(err, result){
            if(err){
                res.send(err);
            }
        });
    }
    res.redirect('genImage1.html');}
});

app.post('/filter', function(req, res){
   filterUsed = req.body.filterNum;
   res.send({
       success: (filterUsed === 1 || filterUsed === 2 || filterUsed === 3)
   });
});

app.get("/justUploaded", function(request, response){
    if (mongoExpressAuth.isLoggedIn(request)){
        mongoExpressAuth.getAccount(request, function(err, result){
            if (err)
                response.send(err);
            else 
                justUploaded = result.currentUpload;
        });
    }
    response.send({
        justUploaded: justUploaded, 
        success: (justUploaded !== "")
    });

});

// Asynchronously read file contents, then call callbackFn
function readFile(filename, defaultData, callbackFn) {
  fs.readFile(filename, "utf8", function(err, data) {
    if (err) {
      console.log("Error reading file: ", filename);
      data = defaultData;
    } else {
      console.log("Success reading file: ", filename);
    }
    if (callbackFn) callbackFn(err, data);
  });
}

app.get("/allImages", function(request,response){
    if (mongoExpressAuth.isLoggedIn(request)){
    var h = mongoExpressAuth.getUserImages(request, function(err, result){
            if(err){
                res.send(err);
            }
        });
    //console.log("Result: "+(h.items));
    }
});

app.post("/save", function(request,response){
    var img = request.body.img;
    var title = request.body.title;
    var desc = request.body.desc;
    if (mongoExpressAuth.isLoggedIn(request)){
        mongoExpressAuth.addImage(request, img, title, desc, function(err, result){
            if(err){
                res.send(err);
            }
        });
    }
})




var library = {"Elephant":"imageLib/elephant.jpg",
               "Letter A":"imageLib/letter_a.jpg",
               "Letter B":"imageLib/letter_b.jpg",
               "Letter C":"imageLib/letter_c.jpg",
               "Letter D":"imageLib/letter_d.jpg",
               "Letter E":"imageLib/letter_e.jpg",
               "Letter F":"imageLib/letter_f.jpg",
               "Letter G":"imageLib/letter_g.jpg",
               "Circle":"imageLib/shape_circle.jpg",
               "Square":"imageLib/shape-square.jpg",
               "Triangle":"imageLib/shape-triangle.jpg"
};


app.get('/imageLib', function(request, response){

    response.send({
        library: library

    })

});

app.get("/converted", function(request, response){
    var text;
    readFile("static/foo.txt","",function(err,data){
        text = data;
        text = text.replace(/ /g,"&nbsp;");
        text = text.replace(/\n/g,"<br />");
        response.send({
            text: text,
            success: true
        });
    });
    
});





app.use(express.static(__dirname + '/static/'));
// Finally, initialize the server, then activate the server at port 8889
