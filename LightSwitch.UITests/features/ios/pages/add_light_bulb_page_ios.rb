require 'calabash-cucumber/ibase'

class AddLightBulbPage < Calabash::IBase
  def title
    "Add Light Bulb"
  end

  def fill_in_field(field, value)
    case field.downcase
      when "name"
        fill_in_name(value)
      when "message"
        fill_in_message(value)
      else
        pending
    end
  end

  def fill_in_name (name)
    pending
  end

  def fill_in_message (message)
    pending
  end

  def add_contact(contact)
    pending
  end

  def click_button(button)
    pending
  end
end