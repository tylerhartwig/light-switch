require 'calabash-cucumber/core'

Before do
  backdoor 'resetDatabase:', ''
end