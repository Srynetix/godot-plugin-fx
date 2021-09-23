shader_type canvas_item;

uniform float strength : hint_range(0, 0.1, 0.001) = 0;
uniform int samples = 11;

void fragment() {
  vec4 color = texture(SCREEN_TEXTURE, SCREEN_UV);
  int pix_count = 1;
  float blur_step = strength / float(samples);
  for (int i = 1; i < samples; ++i) {
    float amount = blur_step * float(i);
    pix_count += 8;
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(-amount, -amount));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(-amount, 0));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(-amount, amount));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(0, -amount));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(0, amount));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(amount, -amount));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(amount, 0));
    color += texture(SCREEN_TEXTURE, SCREEN_UV + vec2(amount, amount));    
  }
  COLOR = color / float(pix_count);
}