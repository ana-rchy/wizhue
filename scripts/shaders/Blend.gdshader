shader_type canvas_item;

uniform sampler2D red;
uniform sampler2D green;
uniform sampler2D blue;

void fragment() {
	COLOR = texture(red, UV) + texture(green, UV) + texture(blue, UV);
}