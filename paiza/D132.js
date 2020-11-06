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
    printStr = "";
    for(let i = 0; i < lines[0]; i++) {
        printStr += lines[1];
    }
    console.log(printStr);
});
