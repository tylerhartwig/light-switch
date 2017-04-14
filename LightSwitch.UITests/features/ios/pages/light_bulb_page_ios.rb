require 'calabash-cucumber/ibase'

class LightBulbPage < Calabash::IBase
  def title
    "Light Bulbs"
  end

  def add_button
    "view marked: '+'"
  end

  def press_add_light_bulb
    touch add_button
  end

  def verify_light_bulb_exists(light_bulb_name)
    wait_for_element_exists "view marked:'#{light_bulb_name}'"
  end
end