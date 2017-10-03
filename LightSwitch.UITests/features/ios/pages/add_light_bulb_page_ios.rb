class AddLightBulbPage < BasePage
  def identifier
    text_exact "Edit Light Bulb"
  end

  def name_field
    first_textfield
  end

  def message_field
    first_ele "XCUIElementTypeTextView"
  end

  def add_contact_button
    "UIButton marked:'Add Contact'"
  end

  def done_button
    "UIButton marked:'Done'"
  end

  def save_button
    button 'Save'
  end

  def save_light_bulb
    save_button.click
  end

  def fill_in_fields_with_light_bulb(light_bulb)
    name_field.type light_bulb.name
    message_field.type light_bulb.message
    # light_bulb.contacts.each { |contact| add_contact(contact) } unless light_bulb.contacts.nil?
  end

  def fill_in_name (name)
    throw Cucumber::Pending
    # wait_tap name_field
    # keyboard_enter_text name
    # hide_keyboard_for_view name_field
  end

  def fill_in_message (message)
    throw Cucumber::Pending
    # wait_tap message_field
    # keyboard_enter_text message
    # hide_keyboard_for_view message_field
  end

  def add_contact(contact)
    throw Cucumber::Pending
    # wait_tap add_contact_button
    # wait_tap "view marked:'#{contact}'"
    # wait_tap done_button, transition_duration: 1
    # wait_for_transition trait
    # check_element_exists "view marked: '#{contact}'"
  end

  def hide_keyboard_for_view(view)
    throw Cucumber::Pending
    # query "#{view} isFirstResponder:1", :resignFirstResponder
  end
end