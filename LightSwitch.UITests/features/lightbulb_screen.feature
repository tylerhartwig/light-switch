Feature: Light Bulb management

  Users should be able to add, remove, or edit the "Light Bulbs" in the app. A "Light Bulb" is the term used by the app
  to combine an accountability message and a list of contacts to send it to into one greater idea. The idea is that a
  single Light Bulb will illuminate an area of the user's life which the user wishes to shed light on.

  Scenario: User is able to add a new Light Bulb
    Given I am on the Light Bulb screen
    When I add a Light Bulb with name "Stealing Cars"
    Then I should see a Light Bulb titled "Stealing Cars"

  Scenario: User is able to delete an already existing Light Bulb
    Given A Light Bulb with name "Stealing Cars" exists
    When I remove the "Stealing Cars" Light Bulb
    Then I should not see a Light Bulb titled "Stealing Cars"