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
    pending
  end
end