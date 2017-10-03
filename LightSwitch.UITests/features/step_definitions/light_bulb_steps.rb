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
  exists { light_bulb_page.identifier }
end

Then(/^I should (not )?see a Light Bulb titled "([^"]*)"$/) do |negated, light_bulb_name|
  light_bulb_page.verify_light_bulb light_bulb_name, negated.nil?
end

When(/^I add a Light Bulb with name "([^"]*)"$/) do |light_bulb_name|
  light_bulb = build :light_bulb, name: light_bulb_name
  add_light_bulb light_bulb
end

Given(/^A Light Bulb with name "([^"]*)" exists$/) do |light_bulb_name|
  light_bulb = build :light_bulb, name: light_bulb_name
  add_light_bulb light_bulb
end

When(/^I remove the "([^"]*)" Light Bulb$/) do |light_bulb_name|
  light_bulb = build :light_bulb, name: light_bulb_name
  light_bulb_page.remove light_bulb
end