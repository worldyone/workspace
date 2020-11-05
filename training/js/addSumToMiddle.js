function addSumToMiddle(array) {
  for (let i = 1; i < array.length; i+=2) {
    array.splice(i, 0, array[i-1] + array[i]);
  }
}
