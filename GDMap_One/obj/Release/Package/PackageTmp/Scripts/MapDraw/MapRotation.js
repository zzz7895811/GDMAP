function mapRotation() {
    if (val >= 360) {
        val = 0;
    }
    else {
        val += 0.1;
    }
    map.setRotation(val);
}