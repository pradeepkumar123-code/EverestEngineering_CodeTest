
# Delivery Cost and Time Estimation Application

## Overview
Kiki, a first-time entrepreneur from Koriko, has launched a distance courier service to deliver packages. To attract customers, her team has introduced offer codes for discounts. Additionally, the service needs to efficiently calculate delivery times using a limited fleet of vehicles. This document provides the functional requirements, algorithms, and technical implementation details for the command-line application designed to meet these requirements.

---

## Functional Requirements

### Part 1: Delivery Cost Estimation with Offers

1. **Input Format**:
    - `base_delivery_cost no_of_packages`
    - For each package: `pkg_id pkg_weight_in_kg distance_in_km offer_code`

2. **Output Format**:
    - For each package: `pkg_id discount total_cost`

3. **Calculation**:
    - Delivery Cost Formula:
      ```
      Total Cost = Base Delivery Cost + (Weight * 10) + (Distance * 5)
      ```
    - Discount Formula:
      ```
      Discount = (Percentage Discount / 100) * Total Cost
      ```
    - Validate offer codes based on predefined criteria.
    - 

### Part 2: Delivery Time Estimation

1. **Input Format**:
    - `base_delivery_cost no_of_packages`
    - Package details as above.
    - Fleet details: `no_of_vehicles max_speed max_carriable_weight`

2. **Output Format**:
    - For each package: `pkg_id discount total_cost estimated_delivery_time_in_hours`

3. **Calculation Criteria**:
    - Each vehicle can carry packages up to its weight limit (`max_carriable_weight`).
    - Vehicles travel at a constant speed (`max_speed`) and return to the hub after each delivery.
    - Delivery time for a package:

      Time = 2 * (Distance / Speed)

    - Prioritize heavier packages. If weights are equal, prioritize by earliest delivery.

## Implementation Plan

### Step 1: Input Parsing
- Parse base delivery cost and package details.
- Validate input formats and handle incorrect inputs gracefully.

### Step 2: Offer Code Validation


### Step 3: Delivery Cost Calculation
- Calculate total cost and discount using the provided formulas.
- Extendable structure for adding new offer codes inside ModelBuilder of DBContext:

### Step 4: Delivery Time Estimation
- Sort packages by weight in descending order.
- Assign packages to vehicles based on their capacity and speed.
- Simulate vehicle trips and calculate estimated delivery times.

### Step 5: Command-Line Interface
- Build an interactive CLI for users to input data and view results.




