Feature: Login Functionality
  As a valid user
  I want to login to the application
  So that I can access my dashboard

  Scenario: Successful login with valid credentials
    Given I navigate to the login page
    When I enter valid username and password
    And I click the login button
    Then I should see the dashboard page


Scenario: Successfully create and manage a new patient visit
    Given I am logged into the application with valid credentials
    When I create a new patient with details:
      | FirstName | LastName | Gender | Age | Contact    |
      | John      | Doe      | Male   | 30  | 03001234567 |
    Then I should see a confirmation message "Patient created successfully"
    When I search for "John Doe" in the patient lookup
    Then the patient "John Doe" should appear in the search results
    When I create a new visit for "John Doe" with reason "General Checkup"
    Then the visit should be created successfully

