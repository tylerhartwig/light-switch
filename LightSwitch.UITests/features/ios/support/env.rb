require 'rspec/expectations'
require 'appium_lib'
require 'cucumber/ast'
require File.join(File.dirname(__FILE__), '..', '..', '..', 'lib', 'light_switch')

# Custom world object
class AppiumWorld
end

device_identifier = ENV['DEVICE']

if device_identifier=='iOS Simulator'
  caps = Appium.load_appium_txt file: File.expand_path("./../../device_config/#{device_identifier}.txt",__FILE__), verbose: true
else
  caps = Appium.load_appium_txt file: File.expand_path("./../../../default_device_config.txt",__FILE__), verbose: true
end

Appium::Driver.new(caps)
Appium.promote_appium_methods AppiumWorld

module PageLoader
  def page(page_class)
    Appium.promote_appium_methods page_class
    page_realized = page_class.new $driver
  end
end

World { AppiumWorld.new }

World(PageLoader)
World(LightBulbHelper)

Before { $driver.start_driver }
After { $driver.driver_quit }
