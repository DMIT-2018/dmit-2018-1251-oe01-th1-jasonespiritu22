<Query Kind="Statements">
  <Connection>
    <ID>763d2992-9533-4984-aae2-0220da817ff2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//Q1
ClubActivities
	.Where(x => x.StartDate >= new DateTime(2025, 1, 1) 
				&& x.CampusVenue.Location != "Scheduled Room" 
				&& x.Name != "BTech Club Meeting")
	.OrderBy(x => x.StartDate)
	.Select(x => new
	{
			StartDate = x.StartDate,
			Location = x.CampusVenue.Location, 
			Club = x.Club.ClubName,
			Activity = x.Name
	})
	.Dump();
	
//Q2
Programs
	.Where(x => x.ProgramCourses.Count(x => x.Required.Equals(true)) >= 22)
	.OrderBy(x => x.ProgramName)
	.Select(x => new
	{
	       School = x.SchoolCode.ToUpper().Contains("SAMIT") ? "School of Advance Media and IT" :
					x.SchoolCode.ToUpper().Contains("SEET") ? "School of Electrical Engineering Technology" : 
					"Unknown",
		   Program = x.ProgramName,
		   RequiredCourseCount = x.ProgramCourses.Count(x => x.Required.Equals(true)),
		   OptionalCourseCount = x.ProgramCourses.Count(x => x.Required.Equals(false))
	})
	.Dump();

//Q3
Students
	.Where(x => x.StudentPayments.Count() == 0
				&& x.Countries.CountryName != "CANADA")
	.OrderBy(x => x.LastName)
	.Select(x => new
	{
		StudentNumber = x.StudentNumber,
		CountryNamae = x.Countries.CountryName,
		FullName = x.FirstName + " " + x.LastName,
		ClubMembershipCount = x.ClubMembers.Count() == 0 ? "None" : 
							  x.ClubMembers.Count.ToString()
	})
	.Dump();

//Q4
Employees
	.Where(x => x.Position.Description == "Instructor" 
				&& x.ReleaseDate == null
				&& x.ClassOfferings.Any())
	.OrderByDescending(x => x.ClassOfferings.Count())
	.ThenBy(x => x.LastName)
	.Select(x => new 
	{
		ProgramName = x.Program.ProgramName,
		FullName = x.FirstName + " " + x.LastName,
		WorkLoad = x.ClassOfferings.Count() > 24 ? "High" :
				   x.ClassOfferings.Count() > 8 ? "Med" : "Low"
	})
	.Dump();
				 
//Q5
Clubs
	.OrderByDescending(x => x.ClubMembers.Count())
	.Select(x => new
	{
	    Supervisor = x.Employee == null ? "Unknown" :
					 x.Employee.FirstName + " " + x.Employee.LastName,
		Club = x.ClubName,
		MemberCount = x.ClubMembers.Count(),
		Activities = x.ClubActivities.Any() ? x.ClubActivities.Count().ToString() : 
		"None Schedule"
	})
	.Dump();