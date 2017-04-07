Given(/^I (?:am|should be) on the ([\w\s]+) screen$/) do |page_name|
  class_name = "#{page_name.delete(" ")}Page"
  @current_page = page(Object.const_get(class_name)).await
end

When(/^I press the add light bulb button$/) do
  @current_page.press_add_light_bulb
end


When(/^I put "([^"]*)" in the "([^"]*)" field$/) do |value, field|
  @current_page.fill_in_field(field, value)
end

And(/^add "([^"]*)" as a contact$/) do |contact_name|
  @current_page.add_contact(contact_name)
end

And(/^I click on the "([^"]*)" button$/) do |button_name|
  @current_page.click_button(button_name)
end

And(/^I should see a "([^"]*)" light bulb$/) do |arg|
  pending
end