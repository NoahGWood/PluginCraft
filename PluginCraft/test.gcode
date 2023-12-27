; G-code for a simple 3D square path

; Move to the starting point
G0 X0 Y0 Z0

; Move to the next point
G1 X10 Y0 Z0

; Move to the next point
G1 X10 Y10 Z0

; Move to the next point
G1 X0 Y10 Z0

; Move back to the starting point, completing the square
G1 X0 Y0 Z0
