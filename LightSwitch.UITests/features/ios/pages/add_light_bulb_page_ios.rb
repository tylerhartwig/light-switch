require 'calabash-cucumber/ibase'

class AddLightBulbPage < Calabash::IBase
  def title
    "Edit Light Bulb"
  end

  def name_field
    "UITextField"
  end

  def message_field
    "UITextView"
  end

  def add_contact_button
    "UIButton marked:'Add Contact'"
  end

  def done_button
    "UIButton marked:'Done'"
  end

  def save_button
    "view marked:'Save'"
  end

  def save_light_bulb
    wait_tap save_button
  end

  def fill_in_fields_with_light_bulb(light_bulb)
    fill_in_name light_bulb[:name]
    fill_in_message light_bulb[:message]
    light_bulb[:contacts].split(',').each do |contact|
      contact.chomp!
      add_contact(contact)
    end
  end

  def fill_in_name (name)
    wait_tap name_field
    keyboard_enter_text name
    hide_keyboard_for_view name_field
  end

  def fill_in_message (message)
    wait_tap message_field
    keyboard_enter_text message
    hide_keyboard_for_view message_field
  end

  def add_contact(contact)
    wait_tap add_contact_button
    wait_tap "view marked:'#{contact}'"
    wait_tap done_button, transition_duration: 1
    wait_for_transition trait
    check_element_exists "view marked: '#{contact}'"
  end

  def hide_keyboard_for_view(view)
    query "#{view} isFirstResponder:1", :resignFirstResponder
  end
end