# Job Dispatcher Demo

The application will take a list of job names (ie. A,b,C,d,e,F,g,H) and a time range.
A job is created for each of the names and the application will execute them in parallel.

Jobs with lower case name should fail right away while the others will sleep for a random period within the time range specified.

Upon the completion of all the jobs, the application should write out the result of each job, including how long the job takes
and the associated error if any.

