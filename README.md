# scheduler-test-project

This is a standalone console executable designed to compute per diem compensation costs based on travel parameters in a simplified GSA reimbursement model.

The project was generated in Visual Studio 2015. To run the project, clone the repository and build from inside Visual Studio.

## Commands

On building, the project will generate an executable, `Scheduler.exe`. This executable takes, as command line arguments, strings referencing JSON input files. Example usage:


```
Scheduler.exe "Sets\Set 1.json" "Sets\Set 2.json"
```

The executable will output to console the final cost.

## Heuristics

The tool computes per diem reimbursement according to a set of rules:

- Costs are dependent on city type: High-cost city or Low-cost city;
- Costs for each day are dependent on whether a day is a travel day or not;
- If any day meets any of the following criteria, it is **not** a travel day; otherwise it is:
  - The day is between the start and end dates, exclusive, of a project;
  - Multiple projects are billed on that day;
  - The day is a start date of a project, but a previous project ended the day before;
  - The day is an end date of a project, but a previous project starts the next day.
- When multiple projects occur on the same day, the higher cost prevails.


## Input

Input is managed using a JSON file with the following structure:

```JSON
[
  {
    "Name": "Project1",
    "StartDate": "2015-09-01T00:00",
    "EndDate": "2015-09-03T00:00",
    "City": "LowCost"
  },
  {
    "Name": "Project2",
    "StartDate": "2015-09-04T00:00",
    "EndDate": "2015-09-06T00:00",
    "City": "LowCost"
  }
]
```

Valid values for the `"City"` field are `"LowCost"` and `"HighCost"`. Dates are input in ISO format.

## Tests

NUnit is used for performing unit tests. A separate project, `SchedulerTest` is included in the solution. The tests evaluate the ability to properly read JSON files (`Test_Input.cs`) and to properly compute costs based on some test cases. The problem sets are not part of the unit tests, however.

## Configuration

A JSON file storing a configuration for each city is used to configure costs. In a practical application, these data would likely be in a largely-static database or other similar format. JSON was chosen here to reduce the package footprint for the codebase.
## Evaluation

The evaluation sets will build with the solution and be stored in the `Sets` subdirectory. These consist of four JSON files, one for each exemplar problem. These exemplar problems have been validated against hand computation according to the given spec.
