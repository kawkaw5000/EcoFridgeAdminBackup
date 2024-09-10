﻿using AdminSideEcoFridge.Models;
using AdminSideEcoFridge.Models.CustomModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AdminSideEcoFridge.Utils;
using AdminSideEcoFridge.Repository;
using Microsoft.AspNetCore.Authorization;

namespace AdminSideEcoFridge.Controllers
{

    public class AccountController : BaseController
    { 

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomUserModelForLogIn user)
        {
            var userObj = _db.Users.Where(model => (model.Email == user.Email || model.Email == user.Email)).FirstOrDefault();

            if (userObj == null || userObj.EmailConfirmed == false)
            {
                return View();
            }

            if (user.Password != userObj.Password)
            {
                return View();
            }

            var userRoleVw = _db.VwUsersRoleViews.Where(m => m.UserId == userObj.UserId).FirstOrDefault();

            if (userRoleVw == null || String.IsNullOrEmpty(userRoleVw.RoleName))
            {
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(userObj.UserId)),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRoleVw.RoleName)

            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            return RedirectToAction("Dashboard", "Home");
         }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        #region Edit Section -
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userRepo.Get(id);            

            if (user == null)
            {
                return NotFound();
            }

            return PartialView("_EditUserPartialView", user);
        }

        [HttpPost]
        public IActionResult Edit(User u)
        {
            if (u == null)
            {
                return RedirectToAction("Login");
            }

            var existingUser = _userRepo.Get(u.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = u.FirstName;
            existingUser.LastName = u.LastName;
            existingUser.Gender = u.Gender;
            existingUser.Birthdate = u.Birthdate;
            existingUser.Street = u.Street;
            existingUser.Barangay = u.Barangay;
            existingUser.City = u.City;
            existingUser.Province = u.Province;

            if (!string.IsNullOrEmpty(u.ProfilePicturePath))
            {
                existingUser.ProfilePicturePath = u.ProfilePicturePath;
            }

            var result = _userRepo.Update(existingUser.UserId, existingUser);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"User {u.Username} Updated";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Unable to update user. Please try again.");
            return View(u);
        }


        [HttpGet]
        public IActionResult EditOrg(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            return PartialView("_EditOrgPartialView", user);
        }

        [HttpPost]
        public IActionResult EditOrg(User u)
        {
            if (u == null)
            {
                return RedirectToAction("Login");
            }

            var existingUser = _userRepo.Get(u.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = u.FirstName;
            existingUser.LastName = u.LastName;
            existingUser.Gender = u.Gender;
            existingUser.Birthdate = u.Birthdate;
            existingUser.Street = u.Street;
            existingUser.Barangay = u.Barangay;
            existingUser.City = u.City;
            existingUser.Province = u.Province;

            if (!string.IsNullOrEmpty(u.ProfilePicturePath))
            {
                existingUser.ProfilePicturePath = u.ProfilePicturePath;
            }

            if (!string.IsNullOrEmpty(u.ProofPicturePath))
            {
                existingUser.ProfilePicturePath = u.ProfilePicturePath;
            }

            var result = _userRepo.Update(existingUser.UserId, existingUser);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"User {u.Username} Updated";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Unable to update user. Please try again.");
            return View(u);
        }

        [HttpGet]
        public IActionResult EditFoodResto(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            return PartialView("_EditFoodOrgPartialView", user);
        }

        [HttpPost]
        public IActionResult EditFoodResto(User u)
        {

            if (u == null)
            {
                return RedirectToAction("Login");
            }

            var existingUser = _userRepo.Get(u.UserId);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = u.FirstName;
            existingUser.LastName = u.LastName;
            existingUser.Gender = u.Gender;
            existingUser.Birthdate = u.Birthdate;
            existingUser.Street = u.Street;
            existingUser.Barangay = u.Barangay;
            existingUser.City = u.City;
            existingUser.Province = u.Province;

            if (!string.IsNullOrEmpty(u.ProfilePicturePath))
            {
                existingUser.ProfilePicturePath = u.ProfilePicturePath;
            }

            if (!string.IsNullOrEmpty(u.ProofPicturePath))
            {
                existingUser.ProfilePicturePath = u.ProfilePicturePath;
            }

            var result = _userRepo.Update(existingUser.UserId, existingUser);

            if (result == ErrorCode.Success)
            {
                TempData["Msg"] = $"User {u.Username} Updated";
                return RedirectToAction("Dashboard", "Home");
            }

            ModelState.AddModelError("", "Unable to update user. Please try again.");
            return View(u);
        }
        #endregion

        #region Account Creation -
        [HttpGet]
        public IActionResult AdminCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminCreate(User user, IFormFile ProfilePicturePath)
        {
            var existingEmail = _db.Users.Where(model => model.Email == user.Email).FirstOrDefault();

            if (existingEmail == null)
            {
          
                user.FirstName = " ";
                user.LastName = " ";
                user.Gender = "M";
                user.Birthdate = DateOnly.FromDateTime(DateTime.Now);
                user.Barangay = " ";
                user.City = " ";
                user.Street = " ";
                user.Province = " ";

   
                if (ProfilePicturePath != null && ProfilePicturePath.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicturePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles/adminProfiles/", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilePicturePath.CopyTo(stream);
                    }

                    user.ProfilePicturePath = "/images/profiles/adminProfiles/" + fileName;
                }
                else
                {
                    user.ProfilePicturePath = "/images/profiles/default.png";
                }

         
                if (_userRepo.Create(user) == ErrorCode.Success)
                {
                    var adminRole = _roleRepo.GetAll().FirstOrDefault(r => r.RoleName == "admin");
                    if (adminRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = user.UserId,
                            RoleId = adminRole.RoleId
                        };
                        _userRoleRepo.Create(userRole);
                    }
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email is already taken.");
                return View(user);
            }
        }


        //[HttpPost]
        //public IActionResult AdminCreate(User user)
        //{
        //    var existingEmail = _db.Users.Where(model => model.Email == user.Email).FirstOrDefault();



        //    if (existingEmail == null)
        //    {
        //        user.FirstName = " ";
        //        user.LastName = " ";
        //        user.Gender = "M";
        //        user.Birthdate = DateOnly.FromDateTime(DateTime.Now);

        //        user.Barangay = " ";
        //        user.City = " ";
        //        user.Street = " ";
        //        user.Province = " ";
        //        if (_userRepo.Create(user) == ErrorCode.Success)
        //        {
        //            var adminRole = _roleRepo.GetAll().FirstOrDefault(r => r.RoleName == "admin");
        //            if (adminRole != null)
        //            {
        //                var userRole = new UserRole
        //                {
        //                    UserId = user.UserId,
        //                    RoleId = adminRole.RoleId
        //                };
        //                _userRoleRepo.Create(userRole);
        //            }
        //            return RedirectToAction("Dashboard", "Home");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Email is already taken.");
        //        return View(user);
        //    }
        //}

        [HttpGet]
        public IActionResult RegularCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegularCreate(User user, IFormFile ProfilePicturePath)
        {
            var existingEmail = _db.Users.Where(model => model.Email == user.Email).FirstOrDefault();

            if (existingEmail == null)
            {
       
                user.FirstName = " ";
                user.LastName = " ";
                user.Gender = "M";
                user.Birthdate = DateOnly.FromDateTime(DateTime.Now);
                user.Barangay = " ";
                user.City = " ";
                user.Street = " ";
                user.Province = " ";

  
                if (ProfilePicturePath != null && ProfilePicturePath.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicturePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles/userProfiles/", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilePicturePath.CopyTo(stream);
                    }

                    user.ProfilePicturePath = "/images/profiles/userProfiles/" + fileName;
                }
                else
                {
                    user.ProfilePicturePath = "/images/profiles/default.png";
                }

       
                if (_userRepo.Create(user) == ErrorCode.Success)
                {
                    var adminRole = _roleRepo.GetAll().FirstOrDefault(r => r.RoleName == "donor");
                    if (adminRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = user.UserId,
                            RoleId = adminRole.RoleId
                        };
                        _userRoleRepo.Create(userRole);
                    }
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email is already taken.");
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult FoodBusinessCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FoodBusinessCreate(User user, IFormFile ProfilePicturePath, IFormFile ProofPicturePath)
        {
            const long MaxFileSize = 2 * 1024 * 1024; // 2 MB
            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var existingEmail = _db.Users.FirstOrDefault(model => model.Email == user.Email);

            if (existingEmail == null)
            {

                user.FirstName = " ";
                user.LastName = " ";
                user.Gender = "M";
                user.Birthdate = DateOnly.FromDateTime(DateTime.Now);    
                
                if (ProfilePicturePath != null && ProfilePicturePath.Length > 0)
                {
                    var profileExtension = Path.GetExtension(ProfilePicturePath.FileName).ToLower();

                    if (!allowedImageExtensions.Contains(profileExtension))
                    {
                        ModelState.AddModelError("", "Profile picture must be a .jpg, .jpeg, or .png file.");
                        return View(user);
                    }

                    if (ProfilePicturePath.Length > MaxFileSize)
                    {
                        ModelState.AddModelError("", "Profile picture must be less than 2 MB.");
                        return View(user);
                    }

                    var profileFileName = "profile_" + Guid.NewGuid() + profileExtension;
                    var profileFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles/foodProfiles/", profileFileName);

                    try
                    {
                        using (var stream = new FileStream(profileFilePath, FileMode.Create))
                        {
                            ProfilePicturePath.CopyTo(stream);
                        }

                        user.ProfilePicturePath = "/images/profiles/foodProfiles/" + profileFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error uploading profile picture: " + ex.Message);
                        return View(user);
                    }
                }
                else
                {
                    user.ProfilePicturePath = "/images/profiles/default.png";
                }

                if (ProofPicturePath != null && ProofPicturePath.Length > 0)
                {
                    var proofExtension = Path.GetExtension(ProofPicturePath.FileName).ToLower();

                    if (!allowedImageExtensions.Contains(proofExtension))
                    {
                        ModelState.AddModelError("", "Proof picture must be a .jpg, .jpeg, or .png file.");
                        return View(user);
                    }

                    if (ProofPicturePath.Length > MaxFileSize)
                    {
                        ModelState.AddModelError("", "Proof picture must be less than 2 MB.");
                        return View(user);
                    }

                    var proofFileName = "proof_" + Guid.NewGuid() + proofExtension;
                    var proofFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/proofs/foodProofs/", proofFileName);

                    try
                    {
                        using (var stream = new FileStream(proofFilePath, FileMode.Create))
                        {
                            ProofPicturePath.CopyTo(stream);
                        }

                        user.ProofPicturePath = "/images/proofs/foodProofs/" + proofFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error uploading proof picture: " + ex.Message);
                        return View(user);
                    }
                }
                else
                {
                    user.ProofPicturePath = "/images/proofs/default.png";
                }

                if (_userRepo.Create(user) == ErrorCode.Success)
                {
                    var donorRole = _roleRepo.GetAll().FirstOrDefault(r => r.RoleName == "food business");
                    if (donorRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = user.UserId,
                            RoleId = donorRole.RoleId
                        };
                        _userRoleRepo.Create(userRole);
                    }

                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error creating user.");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Email is already taken.");
                return View(user);
            }
        }

        //public IActionResult FoodBusinessCreate(User user)
        //{
        //    var existingEmail = _db.Users.Where(model => model.Email == user.Email).FirstOrDefault();

        //    if (existingEmail == null)
        //    {
        //        user.FirstName = " ";
        //        user.LastName = " ";
        //        user.Gender = "M";
        //        user.Birthdate = DateOnly.FromDateTime(DateTime.Now);

        //        if (_userRepo.Create(user) == ErrorCode.Success)
        //        {
        //            var adminRole = _roleRepo.GetAll().FirstOrDefault(r => r.RoleName == "food business");
        //            if (adminRole != null)
        //            {
        //                var userRole = new UserRole
        //                {
        //                    UserId = user.UserId,
        //                    RoleId = adminRole.RoleId
        //                };
        //                _userRoleRepo.Create(userRole);
        //            }
        //            return RedirectToAction("Dashboard", "Home");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Email is already taken.");
        //        return View(user);
        //    }
        //}

        [HttpGet]
        public IActionResult OrganizationCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrganizationCreate(User user, IFormFile ProfilePicturePath, IFormFile ProofPicturePath)
        {
            const long MaxFileSize = 2 * 1024 * 1024; // 2 MB
            var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var existingEmail = _db.Users.FirstOrDefault(model => model.Email == user.Email);

            if (existingEmail == null)
            {

                user.FirstName = " ";
                user.LastName = " ";
                user.Gender = "M";
                user.Birthdate = DateOnly.FromDateTime(DateTime.Now);

                if (ProfilePicturePath != null && ProfilePicturePath.Length > 0)
                {
                    var profileExtension = Path.GetExtension(ProfilePicturePath.FileName).ToLower();

                    if (!allowedImageExtensions.Contains(profileExtension))
                    {
                        ModelState.AddModelError("", "Profile picture must be a .jpg, .jpeg, or .png file.");
                        return View(user);
                    }

                    if (ProfilePicturePath.Length > MaxFileSize)
                    {
                        ModelState.AddModelError("", "Profile picture must be less than 2 MB.");
                        return View(user);
                    }

                    var profileFileName = "profile_" + Guid.NewGuid() + profileExtension;
                    var profileFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles/orgProfiles", profileFileName);

                    try
                    {
                        using (var stream = new FileStream(profileFilePath, FileMode.Create))
                        {
                            ProfilePicturePath.CopyTo(stream);
                        }

                        user.ProfilePicturePath = "/images/profiles/orgProfiles/" + profileFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error uploading profile picture: " + ex.Message);
                        return View(user);
                    }
                }
                else
                {
                    user.ProfilePicturePath = "/images/profiles/default.png";
                }

                if (ProofPicturePath != null && ProofPicturePath.Length > 0)
                {
                    var proofExtension = Path.GetExtension(ProofPicturePath.FileName).ToLower();

                    if (!allowedImageExtensions.Contains(proofExtension))
                    {
                        ModelState.AddModelError("", "Proof picture must be a .jpg, .jpeg, or .png file.");
                        return View(user);
                    }

                    if (ProofPicturePath.Length > MaxFileSize)
                    {
                        ModelState.AddModelError("", "Proof picture must be less than 2 MB.");
                        return View(user);
                    }

                    var proofFileName = "proof_" + Guid.NewGuid() + proofExtension;
                    var proofFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/proofs/organizationProofs/", proofFileName);

                    try
                    {
                        using (var stream = new FileStream(proofFilePath, FileMode.Create))
                        {
                            ProofPicturePath.CopyTo(stream);
                        }

                        user.ProofPicturePath = "/images/proofs/organizationProofs/" + proofFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error uploading proof picture: " + ex.Message);
                        return View(user);
                    }
                }
                else
                {
                    user.ProofPicturePath = "/images/proofs/default.png";
                }

                if (_userRepo.Create(user) == ErrorCode.Success)
                {
                    var donorRole = _roleRepo.GetAll().FirstOrDefault(r => r.RoleName == "donee organization");
                    if (donorRole != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = user.UserId,
                            RoleId = donorRole.RoleId
                        };
                        _userRoleRepo.Create(userRole);
                    }

                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error creating user.");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Email is already taken.");
                return View(user);
            }
        }
        #endregion
    }
}
