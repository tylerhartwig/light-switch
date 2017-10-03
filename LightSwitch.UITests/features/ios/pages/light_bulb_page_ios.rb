class LightBulbPage < BasePage
  def identifier
    text_exact "Light Bulbs"
  end

  def add_button
    button '+'
  end

  def press_add_light_bulb
    add_button.click
  end

  def verify_light_bulb(light_bulb_name, should_be_present = true)
    light_bulb_element = wait { find_exact light_bulb_name }
    if should_be_present
      wait_true { exists { light_bulb_element } }
    else
      wait_true { !exists { light_bulb_element } }
    end
  end

  def remove(light_bulb)
    throw Cucumber::Pending
  end
end