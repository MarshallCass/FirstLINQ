﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users;
            Console.WriteLine(users.ToList().Count);



        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);

            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.

            var products = _context.Products.Where(p => p.Price > 150).ToList();

            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Name } \n {product.Price}");
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.

            var products = _context.Products.Where(p => p.Name.Contains("s")).ToList();

            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Name}");
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.

            var regDate = new DateTime(2016, 1, 1);
            var users = _context.Users.Where(u => u.RegistrationDate < regDate).ToList();

            foreach (User user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);

            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.

            var regDate = new DateTime(2016, 12, 31);
            var regDateTwo = new DateTime(2018, 1, 1);
            var users = _context.Users.Where(u => u.RegistrationDate > regDate && u.RegistrationDate < regDateTwo).ToList();

            foreach (User user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);

            }


        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var userProducts = _context.ShoppingCarts.Include(up => up.Product).Include(up => up.User).Where(up => up.User.Email == "afton@gmail.com");
            foreach (ShoppingCart product in userProducts)
            {
                Console.WriteLine($"Name: {product.Product.Name}, ${product.Product.Price}, Quantity: {product.Quantity}");
            }

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var userProductsSum = _context.ShoppingCarts.Include(ups => ups.Product).Include(ups => ups.User).Where(ups => ups.User.Email == "oda@gmail.com").Select(ups => ups.Product.Price).Sum();
            Console.WriteLine($"Sum total: {userProductsSum}");
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var userId = _context.UserRoles.Include(u => u.User).Where(u => u.RoleId == 2).Select(u => u.User).ToList();
            var employeeShoppingCart = _context.ShoppingCarts.Include(esc => esc.Product).Include(esc => esc.User.UserRoles).Select(esc => new { esc.User, esc.Product, esc.Quantity, esc.User.UserRoles }).ToList();
            foreach (var sc in employeeShoppingCart)
            {
                if (userId.Contains(sc.User))
                {
                    Console.WriteLine($"User Email: {sc.User.Email} Product Name: {sc.Product.Name} Price: ${sc.Product.Price} Quantity in cart: {sc.Quantity}");
                }
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product product = new Product()
            {
                Name = "Good Guy Doll",
                Description = "A great doll who will be your friend to the end",
                Price = 180
            };
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).FirstOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).FirstOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).FirstOrDefault();
            var productId = _context.Products.Where(p => p.Name == "Good Guy Doll").Select(p => p.Id).FirstOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1,
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").FirstOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();

        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.

            var product = _context.Products.Where(u => u.Name == "Good Guy Doll").FirstOrDefault();
            product.Price = 200;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").FirstOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).FirstOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).FirstOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var users = _context.Users.Where(u => u.Email == "oda@gmail.com");
            foreach (User person in users)
            {
                _context.Users.Remove(person);
            }
            _context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            var user = _context.Users.Where(u => u.Email == email && u.Password == password).SingleOrDefault();
            if (user == null)
            {
                Console.WriteLine("Not a real email guy! Try Again!");
            }
            else
            {
                Console.WriteLine("Logged in!");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.
            var users = _context.Users.ToList();
            decimal Total = 0;
            foreach (var user in users)
            {
                var userTotal = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Id == user.Id).Select(sc => sc.Product.Price * sc.Quantity).Sum();
                Console.WriteLine($"Email: {user.Email} Total: ${userTotal}");
                Total += userTotal.Value;
            }
            Console.WriteLine($"Total: ${Total}");
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials
            bool LoggedIn = false;
            User user;

            while (!LoggedIn)
            {
                Console.WriteLine("Enter your Email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine();


                user = _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
                if (user == null)
                {
                    Console.WriteLine("Invalid Email or Password! Try Again!");
                    BonusThree();
                }
                else
                {
                    LoggedIn = true;
                    Console.WriteLine("Logged in!");
                    menu();


                }
            }
        }
        private void seeItemsInCart(User user)
            {

            var userProducts = _context.ShoppingCarts.Include(up => up.Product).Include(up => up.User).Where(up => up.User.Email == user.Email);

                foreach (ShoppingCart product in userProducts)
                {
                    Console.WriteLine($"Name: {product.Product.Name}, ${product.Product.Price}, Quantity: {product.Quantity}");           
                }
                    Console.WriteLine("Press Any Key To Return To Menu.")
                    return;

            }
        private void viewAllProduct()
        {

            var products = _context.Products.Where(p => p.Price > 150).ToList();

            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Name } \n {product.Price}");
            }


        }
            //private void addProductsToCart(User user)
            //{



            //}
            //private void removeItemFromCart(User user)
            //{




            //}
        private void menu()
            {
                int option;
                Console.WriteLine();
                Console.WriteLine("View products in your cart <1>");
                Console.WriteLine("View all Products available <2>");
                Console.WriteLine("Add product to your cart <3>");
                Console.WriteLine("Remove product from cart <4>");
                Console.WriteLine("Log Out <5>");
                Console.WriteLine("");
                Console.Write("Your input:");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        seeItemsInCart(user);
                        break;
                    case 2:
                        viewAllProduct();
                        break;
                    case 3:
                        //addProductsToCart(user);
                        break;
                    case 4:
                        //removeItemFromCart(user);
                        break;
                    case 5:
                        LoggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Wrong entry \n");
                        break;
                }
                Console.WriteLine();
                Console.ReadKey();



            }
        }


    }
}
