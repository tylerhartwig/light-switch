Feature: Light Bulb management

  Users should be able to add, remove, or edit the "Light Bulbs" in the app. A "Light Bulb" is the term used by the app
  to combine an accountability message and a list of contacts to send it to into one greater idea. The idea is that a
  single Light Bulb will illuminate an area of the user's life which the user wishes to shed light on.

  Scenario: User is able to add a new Light Bulb
    Given I am on the Light Bulb screen
      And I press the add light bulb button
      And I am on the Add Light Bulb screen
    When I put "Stealing Cars" in the "Name" field
      And I put "I feel like stealing a car" in the "Message" field
      And add "Paul" as a contact
      And I click on the "Save" button
    Then I should be on the Light Bulb Screen
      And I should see a "Stealing Cars" light bulb

