module LightSwitchDomain
  def light_bulb_page
    @light_bulb_page ||= page(LightBulbPage).await
  end

  def add_light_bulb_page
    @add_light_bulb_page ||= page(AddLightBulbPage).await
  end
end

World(LightSwitchDomain)

Given(/^I (?:am|should be) on the Light Bulb screen$/) do
  wait_for_element_exists(light_bulb_page.trait, timeout: 5)
end


When(/^I add the following Light Bulbs?$/) do |light_bulbs|
  # light_bulb is a table.hashes.keys # => [:name, :message, :contacts]
  light_bulbs.hashes.each do |light_bulb|
    light_bulb_page.press_add_light_bulb
    add_light_bulb_page.fill_in_fields_with_light_bulb light_bulb
    add_light_bulb_page.save_light_bulb
  end
end

Then(/^I should see a Light Bulb titled "([^"]*)" on the list$/) do |light_bulb_name|
  light_bulb_page.verify_light_bulb_exists light_bulb_name
end