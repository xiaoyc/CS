
create database spider

drop table Post
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Title nvarchar(500),
Description nvarchar(1000),
Content nvarchar(5000),
VideoUrl  nvarchar(1000),
Image longblob ,
CategoryId int references Category(Id),
OriginalPageUrl nvarchar(1000),
OriginalPageId int,
IsDraft bit default 1,
CreateTime datetime 
)


alter table post 
add IsDraft bit default 1
INSERT INTO `resource2`.`category`
(
`Title`)
VALUES
(
'tetCatory');

select * from category
create table Category
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Title nvarchar(5000)
)

create table Tag
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Title nvarchar(500)
)

create table PostTag
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
PostId int references Post(Id),
TagId int references Tag(Id)
)


create table Actor
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Name nvarchar(5000)
)

create table PostActor
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
PostId int references Post(Id),
ActorId int references Tag(Id)
)

use resource2
create table JPost
(
Id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
Title nvarchar(500),
Description nvarchar(1000),
Content nvarchar(5000),
VideoUrl  nvarchar(1000),
Image longblob ,
CategoryName nvarchar(500),
Actors nvarchar(500),
Tags nvarchar(500),
IsUploaded bit default 0,
OptionPostId int,
CreateTime datetime 
)

































select * from post

select * from tag



select * from actor

INSERT INTO `resource2`.`actor` (`Name`) VALUES ('actor1');
INSERT INTO `resource2`.`actor` (`Name`) VALUES ('actor2');
INSERT INTO `resource2`.`postactor` (`PostId`, `ActorId`) VALUES ('1', '1');
INSERT INTO `resource2`.`postactor` (`PostId`, `ActorId`) VALUES ('1', '2');
select  * from postactor


select * from posttag
INSERT INTO `resource2`.`posttag` (`PostId`, `TagId`) VALUES ('1', '1');
INSERT INTO `resource2`.`posttag` (`PostId`, `TagId`) VALUES ('1', '2');





















#
1, test, test, test, test, , 1, test, , testtag1, ta2



select p1.*  ,GROUP_CONCAT(actor.name SEPARATOR ', ')  as actors  from (SELECT post.* ,GROUP_CONCAT(tag.Title SEPARATOR ', ')  as tags ,
 
 category.title as categoryName
 FROM post 
 
join posttag on post.id =posttag.postid
join tag on tag.id = posttag.TagId
join category on category.id = post.categoryId

) as p1 

join postactor on p1.id =postactor.postid
join actor on actor.id = postactor.actorId
GROUP BY p1.id



 GROUP_CONCAT(actor.name SEPARATOR ', ')  as actors 


SELECT post.*,
#category.title as categoryName,
 GROUP_CONCAT(tag.Title SEPARATOR ', ')  as tags ,
#GROUP_CONCAT(actor.name SEPARATOR ', ')  as actors 
 FROM post 
join posttag on post.id =posttag.postid
join tag on tag.id = posttag.TagId

#join postactor on post.id =postactor.postid
#join actor on actor.id = postactor.actorId

#join category on category.id = post.categoryId

GROUP BY post.id




















SELECT  post.Id,  tag.title,actor.name

,category.title as categoryName
 
 FROM post 
 
left join posttag on post.id =posttag.postid
left join tag on tag.id = posttag.TagId


left join postactor on post.id =postactor.postid
left join actor on actor.id = postactor.actorId

join category on category.id = post.categoryId

GROUP BY post.id


select * , title2= '33' from post





















select * from post where isdraft=1

























select p1.*  ,GROUP_CONCAT(actor.name SEPARATOR ', ')  as actors  from (SELECT post.* ,GROUP_CONCAT(tag.Title SEPARATOR ', ')  as tags ,
                                     category.title as categoryName
                                     FROM post 
                                    join posttag on post.id = posttag.postid
                                    join tag on tag.id = posttag.TagId
                                    join category on category.id = post.categoryId
                                    where not exists ( select 1 from jPost where title = post.title )
                                    ) as p1
                                    join postactor on p1.id = postactor.postid
                                    join actor on actor.id = postactor.actorId
                                    GROUP BY p1.id




SELECT post.* ,GROUP_CONCAT(tag.Title SEPARATOR ', ')  as tags ,
                                     category.title as categoryName
                                     FROM post where not exists ( select 1 from jPost where title = post.title )
                                    join posttag on post.id = posttag.postid
                                    join tag on tag.id = posttag.TagId
                                    join category on category.id = post.categoryId
 where not exists ( select 1 from jPost where title = post.title )











SELECT post.* ,GROUP_CONCAT(tag.Title SEPARATOR ', ')  as tags ,
                                     category.title as categoryName
                                     FROM post 
                                    join posttag on post.id = posttag.postid
                                    join tag on tag.id = posttag.TagId
                                    join category on category.id = post.categoryId
where not exists ( select 1 from jPost where title = post.title )



select  *    FROM post where not exists ( select 1 from jPost where title = post.title )








