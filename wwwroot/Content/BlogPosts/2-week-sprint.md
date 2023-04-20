# 2-Week Sprint, my collaboration on a CMS

## Introduction
I had the opportunity to contribute to building a full-scale MVC Web Application as part of a development team. My part of the project consisted of a blog management system. The sprint included planning, daily standups, sprint retrospectives, and review. I worked on various user stories on both the front and backend.

### UI Styling

![Screenshot_20221216_123745](https://user-images.githubusercontent.com/115331883/208185012-d49b3d13-7ed2-40e3-a24d-7eeef2332a22.png)

#### Programming the UI with jQuery

I implemented a modal that would popup when the delete button was clicked to confirm with the user if they intended to delete the database object. To do so, I simply put the modal together with HTML and CSS.

```css
.blog_author-index--deleteModalContainer {
    color: black;
    background-color: white;
    border-radius: 3rem;
    width: 40vw;
    height: max-content;
    padding: 2rem;
    display: none;
    flex-direction: column;
    position: fixed;
    left: 0;
    top: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    z-index: 100;
}

.blog_author-index--deleteModalTopRow {
    display: flex;
    justify-content: space-between
}
```

The modal's display attribute was set to none by default. Using jQuery, the modal was programmed to display upon clicking the delete button and to disappear upon confirming deletion or clicking the close button.

```js
//Call upon clicking delete button
function deleteAuthor(authorId) {
    //Define delete modal id
    let deleteModal = '#' + authorId + '-deleteModal'
    //Open delete modal css
    $(deleteModal).show();
    $("body").css("overflow-y", "hidden");
    $(".blog_author-index-authorCardContainer, #createNewButton").css("opacity", "0.5");
}

//Call upon clicking x button in modal
function closeModal(authorId, buttonName) {
    //Define delete modal id
    let deleteModal = '#' + authorId + '-deleteModal'
    //Close delete modal - css
    $(deleteModal).hide();
    $("body").css("overflow-y", "visible");
    $(".blog_author-index-authorCardContainer, #createNewButton").css("opacity", "1");

    //Upon clicking the deleteButton removes author from page and database
    if (buttonName == 'deleteButton') {
        //Fade out animation
        $('.' + authorId).fadeOut(500);
```

### Asynchronous Database Updates with AJAX

The objective here is to "remove" the displayed database objects without requiring a page refresh.

Once users click the delete button, a function is called and the id of the database object is passed. 
This is done so that the database object may be targeted correctly on both front and back ends. 

The button's own id "deleteButton" is also passed since the function's purpose is to close the modal, 
so when other buttons like the "closeButton" would be clicked, the function can differentiate
between them and deliver the correct outcome.


Clicking the delete button would lead the program to this point where the jQuery .ajax()
function is called to perform an asynchronous HTTP request.
The JSON data is posted to the corresponding class controller's method "AsyncDelete". 


```js
$.ajax({
            type: "POST",
            url: "/BlogAuthors/AsyncDelete",
            data: { id: authorId },
        })
```

To allow the objects to display on the page, we must ensure that the "Left" property is nullable,
because it cannot be null if it is not nullable, and the idea here is that only objects with
a null "Left" property can display on the page.

```cs
public DateTime? Left { get; set; }
```

The "AsyncDelete" method takes the id of the object and creates an instance of it with the data from the database.
DateTime.Now is used to update the "Left" property. The property changes are then saved to the database.

```cs
public ActionResult AsyncDelete(int id)
        {
            BlogAuthor blogAuthor = db.BlogAuthors.Find(id);

            blogAuthor.Left = DateTime.Now;

            db.SaveChanges();
```

### Mapping the Database with Entity Framework

```sql
CREATE TABLE [dbo].[BlogAuthors] (
    [BlogAuthorId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [Bio]          NVARCHAR (MAX) NULL,
    [Joined]       DATETIME       NOT NULL,
    [Left]         DATETIME       NULL,
    CONSTRAINT [PK_dbo.BlogAuthors] PRIMARY KEY CLUSTERED ([BlogAuthorId] ASC)
);
```

#### Creating an Area Admin Model

Using the TPH approach, I implemented an admin model named "HeadAuthor" for the project's "Blog" area.

```
        public int ViewsPerMonth { get; set; }
        public int AuthorsHired { get; set; }
        public int AuthorsLetGo { get; set; }
```

### Seeding Data to the Database

To put the new admin class to the test, I added a seed method to the "HeadAuthor" class, followed by an override method in "Configuration.cs" that provides a new implementation of the method as inherited from the base class.


```cs
public static void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("HeadAuthor"))
            {

                var modRole = new IdentityRole();
                modRole.Name = "HeadAuthor";
                roleManager.Create(modRole);

                var headauthor = new HeadAuthor { 
                    
                    UserName = "TheHeadAuthor", Email = "headauthor@theatrecms.dev",
                    ViewsPerMonth = 29, AuthorsHired = 3, AuthorsLetGo = 0 };

                string password = "123";

                var HeadA = roleUserManager.Create(headauthor, password);

                if (HeadA.Succeeded)
                {
                    roleUserManager.AddToRole(headauthor.Id, "HeadAuthor");
                }

            }
```

## Conclusion

Project management is a crucial aspect of programming professionally.
Working in a team and using project management techniques was a wonderful experience for me. It was nice to be able to perform it rather than simply learn about it.

When working on a project that is in the middle of its life cycle, it is important to first grasp the context. The context might be things like the technology being used and the subject of the project itself. You must also acknowledge that the program is not your personal project and that you must execute things in a particular way, even if you don't love the way it's done.

If I could work on this project again, I think looking back there are things I could have implemented in a better way. I believe I could have done better if I had a better understanding of what the following stories would be. I could have been more thoughtful of them during the stories I was working on to make their implementation easier later on. My final takeaway is that I now understand why they say you'll learn most of your skills on the job because I encountered many things that I didn't know exactly how to do but had a general idea of how to do them.