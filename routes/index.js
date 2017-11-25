// import { request } from 'https';

var express = require('express');
var router = express.Router();

var multer = require('multer');
var storage = multer.diskStorage({
  destination: function (req, file, cb) {
    d = Date.now();
    fs.mkdirSync(__dirname + '/../public/uploadedFiles/' + d + '/');
    cb(null, __dirname + '/../public/uploadedFiles/' + d + '/')
  },
  filename: function (req, file, cb) {
    cb(null, file.originalname.substring(0, file.originalname.lastIndexOf('.')).split(' ').join('') + "." + file.originalname.split(".").pop().split(' ').join(''));
    // cb(null, file.originalname.substring(0, file.originalname.lastIndexOf('.')).split(' ').join('') + Date.now() + "." + file.originalname.split(".").pop().split(' ').join(''));
    // cb(null,  Date.now() + "." + file.originalname.split(".").pop())
  }
});

var uploading = multer({
  fileFilter: function (req, file, cb) {
    var UnacceptableMimeTypes = ["exe", "com", "scr", "sh", "ln"]
    if (UnacceptableMimeTypes.indexOf(file.originalname.split(".").pop()) > -1) {
      return cb(new Error('Unacceptable file type.'))
    }

    cb(null, true)
  },
  storage: storage,
  limits: { fileSize: 1000 * 1000 * 20, files: 5 }
});

var noSql = require("nosql");
var db = noSql.load(__dirname + "/../db.nosql");
// db.insert({ id: "fddjsafjdslak", name: "AAA", score: 333 }).callback(function(err) { console.log('The user has been created.'); });

/* GET home page. */
router.get('/', function (req, res, next) {
  res.render('index', { title: 'Express' });
});

router.get('/ranking/getRank', function (req, res, next) {
  // start: int, default 1
  // length: int, default 20

  var start = req.query.start ? parseInt(req.query.start) - 1 : 0;
  var length = req.query.length ? parseInt(req.query.length) : 20;

  db.find().make(function (builder) {
    builder.sort("score", "desc");
    builder.callback(function (err, response) {
      var usedId = [];
      response = response.filter(function (item, index) {
        var isDuplicated = 0;
        isDuplicated = usedId.indexOf(item.id) >= 0;
        console.log(item);
        usedId.push(item.id);
        return !isDuplicated;
      }).filter((item, index) => index >= start && index < start + length);
      fnGetGradeCut(function (stdRank) {
        res.send({
          ranking: response,
          gradeCut: stdRank
        });

      })
    });
  });
});

var fnGetGradeCut = function (callback) {
  db.find().make(function (builder) {
    builder.sort("score", "desc");
    builder.callback(function (err, response) {
      var usedId = [];
      response = response.filter(function (item, index) {
        var isDuplicated = 0;
        isDuplicated = usedId.indexOf(item.id) >= 0;
        console.log(item);
        usedId.push(item.id);
        return !isDuplicated;
      });
      var rate = [.96, .89, .77, .60, .40, .23, .11, .04, 0];
      var stdRank = rate.map(function (r) {
        return {
          grade: rate.indexOf(r) + 1,
          rank: response.length - parseInt(response.length * r),
          score: response[response.length - parseInt(response.length * r) - 1].score
        }
      });
      callback(stdRank);
    });
  });
}

router.get('/ranking/getGradeCut', function (req, res, next) {
  fnGetGradeCut(function (stdRank) {
    res.send(stdRank);
  })
})

router.post('/ranking/submit', uploading.none(), function (req, res, next) {
  // id: str
  // name: str
  // score: int
  (!req.body.id || !req.body.name || !req.body.score) ?
    res.send({
      code: "403",
      message: "wrong data"
    }) :
    db.insert({
      id: req.body.id,
      name: req.body.name,
      score: parseInt(req.body.score),
      time: Date.now()
    }).callback(function (err) {
      console.log(err ? err : "success");
      err ?
        res.send({
          code: 403,
          message: err
        }) :
        res.send({
          code: "200",
          message: "success",
          id: req.body.id,
          name: req.body.name,
          score: parseInt(req.body.score),
          time: Date.now()
        });
    });
});


console.log(Date.now())
module.exports = router;
