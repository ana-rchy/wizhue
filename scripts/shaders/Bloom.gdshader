shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, filter_linear_mipmap;
uniform float blur_scale = 2.0;
uniform float bloom_intensity = 0.25;
uniform float bloom_threshold = 1.0;
uniform vec3 colour_amplification;

void fragment() {
    vec3 colour_original = texture(screen_texture, SCREEN_UV).rgb;
    vec3 colour_blurred = textureLod(screen_texture, SCREEN_UV, blur_scale).rgb;
	colour_blurred = max(colour_blurred - bloom_threshold, 0.0);
	colour_blurred *= colour_amplification;
    vec3 bloom = colour_original + (colour_blurred * bloom_intensity);
	COLOR.rgb = bloom;
}