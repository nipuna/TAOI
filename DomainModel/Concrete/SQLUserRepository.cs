using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.Concrete
{
    public class SQLUserRepository : EntityContainer , IUserRepository
    {
        #region IUserRepository Members

        #region get All Users
        /// <summary>
        /// Returns All the Users
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> getUsers()
        {
            refereshEntities();
            var Users = (from user in _entities.Users
                          select user).AsQueryable();
            
            
            return Users.OrderBy(user => user.Username);

        }
        #endregion

        #region get Users As IQueryable for displaying in the JQGrid
        /// <summary>
        /// get Users As IQueryable for displaying in the JQGrid
        /// </summary>
        /// <param name="Users"> IQueryable<User> All the Users </param>
        /// <returns>IQueryable<userDisplay></returns>
        public IQueryable<userDisplay> getUsersForDisplay(IQueryable<User> Users)
        {
            List<User> lstUsers = Users.ToList();
            List<userDisplay> rows = new List<userDisplay>();
            
            foreach (var user in Users)
            {
                #region Get Brands as string
                string allBrands = "";
                user.Brands.Load();
                Brand[] brands = user.Brands.ToArray();
                foreach (var brand in brands)
                {
                    allBrands = allBrands + brand.Name + ",";
                }
                if (!String.IsNullOrEmpty(allBrands))
                {
                    allBrands = allBrands.Substring(0, allBrands.Length - 1);
                }
                #endregion

                rows.Add(new userDisplay
                    {
                        ID = user.ID,
                        Active = user.Active == true ? "<label class=\"active\"> Yes </label>" : "<label class=\"notactive\"> No </label>",
                        Name = user.Contact == null ? " " : user.Contact.Forename + " " + user.Contact.Surname,
                        Username = user.Username,
                        Email = user.Contact == null ? " " : user.Contact.Email,
                        Phone = user.Contact == null ? " " : user.Contact.Phone,
                        Brand = allBrands,
                        Location = _entities.Countries.Where(country => country.ID == user.CountryID).First().Name,
                        Actions = "<div style=\"width:126px;margin-left:auto;margin-right:auto;\" ><a href=\"/Users/Edit?Id=" + user.ID.ToString() + "\" class=\"btnedit\" style=\"color:#fff\" >Edit</a>" +
                           "<a href=\"/Users/Delete?Id=" + user.ID.ToString() + "\" class=\"btndelete\" style=\"color:#fff\" onclick=\"return deleteConfirmation()\" >Delete</a></div>"
                    });
            }

            return rows.AsQueryable();
        }
        #endregion

        #region Edit User
        /// <summary>
        /// Edits the User based upon the ID value passed
        /// </summary>
        /// <param name="UserId">Id of the User to edit</param>
        public void editUser(int UserId)
        {
            var User = from b in _entities.Users
                        where b.ID == UserId
                        select b;


        }

        #endregion

        #region Get User
        /// <summary>
        /// Gets User details for User with the passes userId
        /// </summary>
        /// <param name="UserId">Id of the user</param>
        /// <returns></returns>
        public IQueryable<User> getUser(int UserId)
        {
            refereshEntities();
            var User = (from b in _entities.Users
                        where b.ID == UserId
                        select b).AsQueryable();

            return User;
        }

        #endregion

        #region Save User

        public void saveUser(User user)
        {

            var User = (from b in _entities.Users
                         where b.ID == user.ID
                         select b).First();
            User = user;
            //User.Logo = imgPath;
            _entities.SaveChanges();

        }

        #endregion

        #region Delete User
        /// <summary>
        /// Deletes an existing User
        /// </summary>
        /// <param name="UserId">Id of the User to be deleted</param>
        public void deleteUser(int UserId)
        {
            var User = (from b in _entities.Users
                         where b.ID == UserId
                         select b).First();
            _entities.DeleteObject(User);
            _entities.SaveChanges();
        }

        #endregion

        #region Create User
        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="newUser">User entity instance to be added</param>
        public void createUser(User newUser)
        {
            //var User = new User();
            //var lastUser = _entities.Users.ToList().Last();
            //User.ID = lastUser.ID + 1;
            //User.Username = name;
            //User.Logo = path;
            _entities.Users.AddObject(newUser);
            _entities.SaveChanges();
        }

        #endregion
        
        #region Gets all the details required for rendering the Edit UI
        /// <summary>
        /// Gets all the details required for rendering the Edit UI
        /// </summary>
        public Dictionary<string, List<details>> getAllUserEditDetails(User user)
        {
            Dictionary<string, List<details>> allDetails = new Dictionary<string, List<details>>();
            User contextUser = _entities.Users.Where(userCnt => userCnt.ID == user.ID).First(); 
            #region get all Brands
            //get all the existing Brands for the User
            contextUser.Brands.Load();
            //user.Brands.Load();
            Brand[] userBrands = contextUser.Brands.OrderBy(brand => brand.Name).ToArray();
            //get all the brands
            //Brand[] brandsAO = _entities.Brands.OrderBy(brand => brand.Name).ToArray();
            Brand[] brandsA = (from b in _entities.Brands
                               from c in b.Customers
                               select b).ToArray();
            //create custom list of brands
            List<details> brandsDetails = new List<details>();
            //Add 'All' to the collection
            brandsDetails.Add(new details(0 , "All" ,false ));
            foreach (var brand in brandsA)
            {
                details brandDetail; 
                if (userBrands.Contains(brand))
                {
                    brandDetail = new details(brand.ID, brand.Name , true);
                }
                else
                {
                    brandDetail = new details(brand.ID, brand.Name, false);
                }
                brandsDetails.Add(brandDetail);
            }

            allDetails.Add("brands", brandsDetails);
            #endregion

            #region get all Languages
            //get all the existing cultures for the User
            contextUser.Cultures.Load();
            Culture[] userCulture = contextUser.Cultures.OrderBy(culture => culture.Locale).ToArray();
            //get all the cultures
            Culture[] culturesA = _entities.Cultures.Where(culture => culture.IsSupported == true).OrderBy(culture => culture.Locale).ToArray();
            //create custom list of brands
            List<details> culturesDetails = new List<details>();
            //Add 'All' to the collection
            culturesDetails.Add(new details(0, "All", false));
            foreach (var culture in culturesA)
            {
                details cultureDetail;
                if (userCulture.Contains(culture))
                {
                    cultureDetail = new details(culture.ID, culture.Locale, true);
                }
                else
                {
                    cultureDetail = new details(culture.ID, culture.Locale, false);
                }
                culturesDetails.Add(cultureDetail);
            }
            allDetails.Add("cultures", culturesDetails);
            #endregion

            #region get all Regions
            //get all the existing Regions for the User
            contextUser.Regions.Load();
            Region[] userRegion = contextUser.Regions.OrderBy(region => region.Name).ToArray();
            //get all the regions from the database
            Region[] regionsA = _entities.Regions.OrderBy(Region => Region.Name).ToArray();
            //create custom list of regions
            List<details> regionsDetails = new List<details>();
            //Add 'All' to the collection
            regionsDetails.Add(new details(0, "All", false));

            foreach (var region in regionsA)
            {
                details regionDetail;
                if (userRegion.Contains(region))
                {
                    regionDetail = new details(region.ID, region.Name, true);
                }
                else
                {
                    regionDetail = new details(region.ID, region.Name, false);
                }
                regionsDetails.Add(regionDetail);

            }
            allDetails.Add("regions", regionsDetails);
            #endregion

            return allDetails;
        }
        #endregion

        
        #region Gets all the details required for rendering the Edit UI
        /// <summary>
        /// Gets all the details required for rendering the Edit UI
        /// </summary>
        public List<userRightdetails> getAllUserRightDetails(User user)
        {
            Dictionary<string, List<details>> allDetails = new Dictionary<string, List<details>>();
            List<userRightdetails> userRightDetails = new List<userRightdetails>();

            #region user detaild for Creating new User
            if (user == null)
            {
                #region get all Brands
                UICategory[] userRightsA = _entities.UICategories.ToArray();

                foreach (var right in userRightsA)
                {
                    userRightdetails userRightDetail;
                    userRightDetail = new userRightdetails(right.ID, right.ParentID, right.LevelInTree, right.PositionInBranch, right.Name, false);
                    userRightDetails.Add(userRightDetail);
                }

                #endregion
            }
            #endregion
            #region user detaild for Editing new User
            else
            {

                //Dictionary<string, List<details>> allDetails = new Dictionary<string, List<details>>();
                User contextUser = _entities.Users.Where(userCnt => userCnt.ID == user.ID).First();

                #region get all User rights
                //get all the existing User rights for the User
                contextUser.UICategories.Load();
                //user.Brands.Load();
                UICategory[] userRights = contextUser.UICategories.ToArray();
                //get all the User rights for the User
                UICategory[] userRightsA = _entities.UICategories.ToArray();
                //create custom list of User rights for the User
                //Add 'All' to the collection
                //userRightDetails.Add(new details(0, "All", false));
                foreach (var right in userRightsA)
                {
                    userRightdetails userRightDetail;

                    if (userRights.Contains(right))
                    {
                        userRightDetail = new userRightdetails(right.ID, right.ParentID, right.LevelInTree, right.PositionInBranch, right.Name, true);
                    }
                    else
                    {
                        userRightDetail = new userRightdetails(right.ID, right.ParentID, right.LevelInTree, right.PositionInBranch, right.Name, false);
                    }
                    userRightDetails.Add(userRightDetail);
                }

                #endregion
            }
            #endregion

            return userRightDetails;
        }
        #endregion

        #region Gets all the details required for rendering the Create UI
        /// <summary>
        /// Gets all the details required for rendering the Create UI
        /// </summary>
        public Dictionary<string, List<details>> getAllUserCreateDetails()
        {
            Dictionary<string, List<details>> allDetails = new Dictionary<string, List<details>>();

            #region get all Brands

            //Brand[] BrandsA = _entities.Brands.OrderBy(brand => brand.Name).ToArray();
            Brand[] BrandsA = (from b in _entities.Brands
                               from c in b.Customers
                               select b).ToArray();
            List<details> BrandsDetails = new List<details>();

            BrandsDetails.Add(new details(0, "All", false));

            foreach (var brand in BrandsA)
            {
                details brandDetail;
                brandDetail = new details(brand.ID, brand.Name, false);
                BrandsDetails.Add(brandDetail);
            }
            allDetails.Add("brands", BrandsDetails);
            #endregion

            #region get all Languages

            Culture[] culturesA = _entities.Cultures.Where(culture => culture.IsSupported == true).OrderBy(culture => culture.Locale).ToArray();
            //create custom list of Brands
            List<details> culturesDetails = new List<details>();
            //Add 'All' to the collection
            culturesDetails.Add(new details(0, "All", false));
            foreach (var culture in culturesA)
            {
                details cultureDetail;
                cultureDetail = new details(culture.ID, culture.Locale, false);
                culturesDetails.Add(cultureDetail);
            }
            allDetails.Add("cultures", culturesDetails);
            #endregion

            #region get all Regions

            Region[] regionsA = _entities.Regions.OrderBy(region => region.Name).ToArray();
            //create custom list of regions
            List<details> regionsDetails = new List<details>();
            //Add 'All' to the collection
            regionsDetails.Add(new details(0, "All", false));
            foreach (var region in regionsA)
            {
                details regionDetail;
                regionDetail = new details(region.ID, region.Name, false);
                regionsDetails.Add(regionDetail);
            }
            allDetails.Add("regions", regionsDetails);
            #endregion

            return allDetails;
        }
        #endregion

        #region Fills/Edits the user and the related entites with the data choosen by the User
        /// <summary>
        /// Fills/Edits the user and the related entites with the data choosen by the User
        /// </summary>
        /// <param name="user">user entity to be modified</param>
        /// <param name="brandChoosen">List containing Id's of the Brands choosen</param>
        /// <param name="cultureChoosen">List containing Id's of the cultures choosen</param>
        /// <param name="regionChoosen">List containing Id's of the coutries choosen</param>
        /// <returns></returns>
        public User editedUserData(User user, List<Int32> brandChoosen, List<Int32> checkboxtree, List<Int32> cultureChoosen, List<Int32> regionChoosen)
        {
            User editedUser = _entities.Users.Where(userCnt => userCnt.ID == user.ID).First();
            editedUser.Username = user.Username;
            editedUser.Password = user.Password;
            editedUser.Active = user.Active;
            editedUser.Comment = user.Comment;
            editedUser.CountryID = user.CountryID ;

            #region brand
            editedUser.Brands.Load();
            editedUser.Brands.Clear(); 
            if (brandChoosen != null)
            {
                List<Brand> BrandsAssct = editedUser.Brands.ToList();
                foreach (var id in brandChoosen)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    if (!BrandsAssct.Contains(inCheck))
                    {
                        editedUser.Brands.Add(inCheck);
                        //editedUser.Brands.Attach(inCheck);
                    }
                }
            }
            #endregion

            #region UserRights
            editedUser.UICategories.Load();
            editedUser.UICategories.Clear();
            if (checkboxtree != null)
            {
                List<UICategory> rightsAss = editedUser.UICategories.ToList();
                foreach (var id in checkboxtree)
                {
                    UICategory inCheck = _entities.UICategories.Where(cat => cat.ID == id).First();
                    if (!rightsAss.Contains(inCheck))
                    {
                        editedUser.UICategories.Add(inCheck);
                        //editedUser.Brands.Attach(inCheck);
                    }
                }
            }
            #endregion

            #region Languages
            editedUser.Cultures.Load();
            editedUser.Cultures.Clear();
            if (cultureChoosen != null)
            {
                List<Culture> culturesAssct = editedUser.Cultures.ToList();
                foreach (var id in cultureChoosen)
                {
                    Culture inCheck = _entities.Cultures.Where(culture => culture.ID == id).First();
                    if (!culturesAssct.Contains(inCheck))
                    {
                        editedUser.Cultures.Add(inCheck);
                    }
                }
            }
            #endregion

            #region regions
            editedUser.Regions.Load();
            editedUser.Regions.Clear();
            if (regionChoosen != null)
            {
                List<Region> regionsAssct = editedUser.Regions.ToList();
                foreach (var id in regionChoosen)
                {
                    Region inCheck = _entities.Regions.Where(region => region.ID == id).First();
                    if (!regionsAssct.Contains(inCheck))
                    {
                        editedUser.Regions.Add(inCheck);
                    }
                }
            }
            #endregion
            
            return editedUser;
        }
        #endregion

        #region Fills/Creates the user and the related entites with the data choosen by the User
        /// <summary>
        /// Fills/Creates the user and the related entites with the data choosen by the User
        /// </summary>
        /// <param name="user">user entity to be modified</param>
        /// <param name="brandChoosen">List containing Id's of the Brands choosen</param>
        /// <param name="cultureChoosen">List containing Id's of the cultures choosen</param>
        /// <param name="regionChoosen">List containing Id's of the coutries choosen</param>
        /// <returns></returns>
        public User createdUserData(User user, List<Int32> brandChoosen, List<Int32> checkboxtree, List<Int32> cultureChoosen, List<Int32> regionChoosen)
        {
            User createdUser = new User();
            createdUser.Username = user.Username;
            createdUser.Password = user.Password;
            createdUser.Active = user.Active;
            createdUser.Comment = user.Comment;
            createdUser.CountryID = user.CountryID;

            #region brand
            //createdUser.Brands.Load();
            //createdUser.Brands.Clear();
            if (brandChoosen != null)
            {
                List<Brand> BrandsAssct = createdUser.Brands.ToList();
                foreach (var id in brandChoosen)
                {
                    Brand inCheck = _entities.Brands.Where(brand => brand.ID == id).First();
                    if (!BrandsAssct.Contains(inCheck))
                    {
                        createdUser.Brands.Add(inCheck);
                        //editedUser.Brands.Attach(inCheck);
                    }
                }
            }
            #endregion

            #region UserRights
            //createdUser.UICategories.Load();
            //createdUser.UICategories.Clear();
            if (checkboxtree != null)
            {
                List<UICategory> rightsAss = createdUser.UICategories.ToList();
                foreach (var id in checkboxtree)
                {
                    UICategory inCheck = _entities.UICategories.Where(cat => cat.ID == id).First();
                    if (!rightsAss.Contains(inCheck))
                    {
                        createdUser.UICategories.Add(inCheck);
                        //editedUser.Brands.Attach(inCheck);
                    }
                }
            }
            #endregion
            #region Languages
            //createdUser.Cultures.Load();
            //createdUser.Cultures.Clear();
            if (cultureChoosen != null)
            {
                List<Culture> culturesAssct = createdUser.Cultures.ToList();
                foreach (var id in cultureChoosen)
                {
                    Culture inCheck = _entities.Cultures.Where(culture => culture.ID == id).First();
                    if (!culturesAssct.Contains(inCheck))
                    {
                        createdUser.Cultures.Add(inCheck);
                        //editedUser.Cultures.Attach(inCheck);
                    }
                }
            }
            #endregion

            #region Regions
            //createdUser.Regions.Load();
            //createdUser.Regions.Clear();
            if (regionChoosen != null)
            {
                List<Region> regionsAssct = createdUser.Regions.ToList();
                foreach (var id in regionChoosen)
                {
                    Region inCheck = _entities.Regions.Where(region => region.ID == id).First();
                    if (!regionsAssct.Contains(inCheck))
                    {
                        createdUser.Regions.Add(inCheck);
                    }
                }
            }
            #endregion

            return createdUser;
        }
        #endregion

        private void refereshEntities()
        {
            _entities.Refresh(System.Data.Objects.RefreshMode.StoreWins, _entities.Users);
            _entities.Refresh(System.Data.Objects.RefreshMode.StoreWins, _entities.Contacts);
            _entities.Refresh(System.Data.Objects.RefreshMode.StoreWins, _entities.Brands);
            _entities.Refresh(System.Data.Objects.RefreshMode.StoreWins, _entities.Cultures);
        }

        #region Authenticates the User
        /// <summary>
        /// Authentication the credentials of the User
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="password">Password of the user</param>
        /// <param name="userId">userid of the user</param>
        /// <returns>bool value whether the user credentials are correct</returns>
        public Boolean authentucateUser(string username, string password , out Int32 userId, out string nameOfUser)
        {
            var usr = _entities.Users.Where(user => user.Username == username && user.Password == password);
            _entities.Refresh(System.Data.Objects.RefreshMode.StoreWins, _entities.Contacts);
            if (usr.Count() != 0)
            {
                userId = usr.First().ID;
                nameOfUser = usr.First().Contact.Forename + " " + usr.First().Contact.Surname;
                return true;
            }
            else
            {
                userId = -1;
                nameOfUser = "";
                return false;
            }
        }
        #endregion

        #endregion

    }
}

#region Display details of the user
/// <summary>
/// Custom object for repersenting the data to be displayed in the JQGrid
/// </summary>
public class userDisplay
{
    public int ID { get; set; }
    public string Active { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Brand { get; set; }
    public string Location { get; set; }
    public string Actions { get; set; }
}

#endregion

#region Display details of the user
/// <summary>
/// Custom Object for filling details of various entities related to the user for rendering Edit and Create UI's
/// </summary>
public partial class details
{
    public details(Int32 id, string name, Boolean status)
    {
        this.ID = id;
        this.Name = name;
        this.Status = status;
    }
    public int? ID { get; set; }
    public string Name { get; set; }
    public Boolean? Status { get; set; }

}


/// <summary>
/// Custom Object for filling details of various entities related to the user for rendering Edit and Create UI's
/// </summary>
public partial class userRightdetails
{
    public userRightdetails(Int32 id, Int32? parentID, Int32 levelInTree, Int32 positionInBranch, string name, Boolean status)
    {
        this.ID = id;
        this.ParentID = parentID;
        this.LevelInTree = levelInTree;
        this.PositionInBranch = positionInBranch;
        this.Name = name;
        this.Status = status;
    }
    public int ID { get; set; }
    public string Name { get; set; }
    public int? ParentID { get; set; }
    public int LevelInTree { get; set; }
    public int PositionInBranch { get; set; }
    //public string Icon { get; set; }
    public Boolean? Status { get; set; }

}
#endregion
