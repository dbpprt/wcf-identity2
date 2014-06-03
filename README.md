wcf-identity2
=============

Shows how to use ASP.NET Identity 2.0 over WCF (trusted subsystem at the moment)
WCF services are extensionless was services (i love autofac :))


The application consists of 3 layers:

  -> the presentation layer : web frontend
  
  -> business layer : contains services with business logic, consumes a data abstraction layer over entityframework,
                      UoW pattern
                      
  -> some infrastructure modules: usermanagement, rolemanagement, repositories, dbcontext base classes, etc.. 
  
All models and dtos are in a seperated library, models are reused as data transfer objects, because i dont
like the seperation of concerns with models. mapping models to dtos is kind of "over-seperated" to me. still keep 
in mind => code less :)

still todo:
=============

  -> some methods are still not implemented
  
  -> project isnt well structured completely
  
  -> some very ugly constructs
  
  -> web frontend contains nothing.. only the service registrations and a controller to test some stuff
  
  -> more to come :)

credits:
=============
https://github.com/imranbaloch/ASPNETIdentityWithOnion
