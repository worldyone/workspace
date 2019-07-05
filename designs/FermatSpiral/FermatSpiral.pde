int itr = 0;
float scalar = 5;
void setup(){
  size(500,500);
  background(255);
}

void draw(){
  translate(width / 2, height / 2);
  fill(0);
  drawFermatSpiral(17.0 / 55);
  itr++;
}

void drawFermatSpiral(float rot){
  float theta = 2 * PI * itr * rot;
  PVector v = PVector.fromAngle(theta);
  v.mult(scalar * sqrt(itr));
  ellipse(v.x, v.y, scalar, scalar);
}
