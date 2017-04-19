require 'factory_girl'

World(FactoryGirl::Syntax::Methods)

FactoryGirl.define do
  factory :light_bulb do
    name "Stealing Cars"
  end

end