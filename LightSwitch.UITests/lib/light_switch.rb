module LightBulbHelper
  def add_light_bulb(light_bulb)
    light_bulb_page.press_add_light_bulb
    add_light_bulb_page.fill_in_fields_with_light_bulb light_bulb
    add_light_bulb_page.save_light_bulb
  end
end