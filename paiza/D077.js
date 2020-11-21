process.stdin.resume();
process.stdin.setEncoding('utf8');

var lines = [];
var reader = require('readline').createInterface({
  input: process.stdin,
  output: process.stdout
});
reader.on('line', (line) => {
  lines.push(line);
});
reader.on('close', () => {
  let line = lines[0].split(" ");
  let ans = line[0] * line[1];
  if( ans > 9999 ) {
      console.log("NG");
  } else {
    console.log(ans);
  }
});
