Feature: Light Bulb management

  Users should be able to add, remove, or edit the "Light Bulbs" in the app. A "Light Bulb" is the term used by the app
  to combine an accountability message and a list of contacts to send it to into one greater idea. The idea is that a
  single Light Bulb will illuminate an area of the user's life which the user wishes to shed light on.

  Scenario: User is able to add a new Light Bulb
    Given I am on the Light Bulb screen
    When I add the following Light Bulb
      | name          | message                              | contacts       |
      | Stealing Cars | Help man, I feel like stealing a car | John Appleseed |
    Then I should see a Light Bulb titled "Stealing Cars" on the list
