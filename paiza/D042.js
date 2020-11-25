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
  let line1 = lines[0].split(" ");
  let line2 = lines[1].split(" ");
  console.log(line1[0]*line2[1] - line1[1]*line2[0]);
});
