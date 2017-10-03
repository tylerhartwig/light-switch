require 'appium_lib'
require 'cucumber/core'

class BasePage
  attr_accessor :driver
  attr_accessor :identifier

  def initialize(driver)
    @driver = driver
  end

  def await
    wait_true { exists { identifier } }
    self
  end
end