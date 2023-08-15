USE [BE-Capstone];
GO

set identity_insert [UserType] on
insert into [UserType] ([Id], [Type]) VALUES (1, 'Admin'), (2, 'Patient'), (3, 'Provider');
set identity_insert [UserType] off

--trying to utilize n'unicode' to add the icons to the database
set identity_insert [Reaction] on
insert into [Reaction] ([Id], [Name], [IconCode])
values (1, 'Thumbs-up', N'U+F406'), (2, 'Thumbs-down', N'U+F404')
set identity_insert [Reaction] off

--aiming for roughly 24 exercises for 3 regimens
--go back and fix the image links to gifs fromthumbs.gyfcat
set identity_insert [Exercise] on
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (1, 'Squat', 'Strengthen', 'Quadriceps', 
'1. Stand up with your feet shoulder-width apart.'  + CHAR(13)+CHAR(10) + 
'2. Bend your knees, press your hips back and stop the movement once the hip joint is slightly lower than the knees or to your comfort level.'  + CHAR(13)+CHAR(10) + 
'3. Press your heels into the floor to return to the initial position.'  + CHAR(13)+CHAR(10) + 
'4. Repeat until set is complete.', 'https://thumbs.gfycat.com/FirmLateCrane.webp');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (2, 'Terminal Knee Extension', 'Strengthen', 'Quadriceps', '
1. Tie an elastic exercise band ,of appropriate weight, to or around something solid.
2. Once secured, loop the band around the back of your knee and step back until you feel tension on the band.
3. Slightly bend this knee lifting the heel off the ground.
4. Then push the heel down into the ground to straighten the knee.
5. Squeeze the quadriceps in this position, keeping bodyweight through this leg.', 'https://thumbs.gfycat.com/PowerlessMajesticDutchsmoushond.webp');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (3, 'Towel Stretch', 'Stretch', 'Calves', '
1. Sit on the floor with your legs extended straight out in front of you.
2. Wrap a towel around your toes on both feet.
3. Pull back slightly until you start to feel a stretch at the very bottom of your feet and the back of your lower legs.', 'https://thumbs.gfycat.com/WavyGoodnaturedCopepod.webp');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (4, 'Standing Soleus Stretch', 'Stretch', 'Calves', '
1. Stand a few feet away from a wall or other support, facing it.
2. Place one leg in back with your heel flat on the floor.
3. Your other leg can come forward toward the support.
4. Gently turn the foot on your injured leg inward toward the other foot.
5. Then slightly bend your front knee into the support until you feel a stretch in your injured leg.', 'https://thumbs.gfycat.com/HonoredScentedHusky.webp');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (5, 'Ankle Circles', 'Stretch', 'Calves', '
1. Start by turning your ankle around slowly in circles to the left, then the right.
2. You may even find it easier to try drawing the alphabet in the air with your foot. Lead with your big toe.
3. Keep your movements small and focus on only use your foot and ankle, not your entire leg.', 'https://images-prod.healthline.com/hlcmsresource/images/topic_centers/Fitness-Exercise/400x400_8_Ankle_Stretches_to_Try_at_Home_Ankle-Circles.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (6, 'Toe Walk', 'Strengthen', 'Calves', '
1. While standing on your toes walk while maintaining your heel off the ground.', 'https://thumbs.gfycat.com/SoreMemorableAmethystsunbird-size_restricted.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (7, 'Heel Walk', 'Strengthen', 'Foot Extensors', '
1. While standing on your heels walk while maintaining your toes off the ground.', 'https://thumbs.gfycat.com/CourageousShrillChimneyswift-size_restricted.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (8, 'Lunges', 'Strengthen', 'Quadriceps', '
1. Start with one foot in front of the other, with your toes facing forward.
2. Keep your back straight.
3. Bend your back knee down so that it almost touches the floor.
4. Then push yourself up again.', 'https://thumbs.gfycat.com/UnknownPleasedKodiakbear-size_restricted.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (9, 'Shoulder Pass-Through', 'Stretch', 'Rotator Cuff', '
1. Stand with your feet shoulder-width apart and your arms in front of your body.
2. Hold a stick, like a broomstick or PVC pipe, with an overhand grip. Your arms will be wider than shoulder-width. Make sure the stick or pipe is parallel to the floor.
3. Engage your core and slowly raise the broomstick or pipe above your head, keeping your arms straight. Only go as far as comfortable.
4. Hold the pose for a few seconds.
5. Return to the starting position.', 'https://thumbs.gfycat.com/TotalAngelicDuckbillplatypus-small.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (10, 'Standing Arm Swings', 'Stretch', 'Rotator Cuff', '
1. Stand tall with your arms by your sides.
2. Engage your core and swing your arms forward until they’re as high as you can go. Make sure you don’t raise your shoulders.
3. Return your arms to the starting position and repeat.', 'https://thumbs.gfycat.com/EvenAjarCanine-small.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (11, 'High-to-Low Rows', 'Strengthen', 'Lats', '
1. Secure a resistance band to a sturdy object above shoulder height.
2. Kneel down on one knee and grab the band with the opposite hand. The other hand can rest at your side.
3. Pull the band toward your body while keeping your torso and arm straight. Focus on squeezing the shoulder blades together.
4. Return to the starting position and repeat.', 'https://post.healthline.com/wp-content/uploads/2020/01/400x400_5_Easy_Rotator_Cuff_Exercises_High_to_Low_Rows.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (12, 'Reverse Fly', 'Strengthen', 'Lats', '
1. Hold a dumbbell in each hand.
2. Stand with your feet shoulder-width apart, knees slightly bent.
3. Engage your core and bend forward at the waist. Keep your back straight. Your arms will be extended.
4. Raise your arms away from your body. Focus on squeezing your shoulder blades together. Stop when you get to shoulder height.
5. Slowly return to the starting position and repeat.', 'https://post.healthline.com/wp-content/uploads/2019/12/400x400-Reverse_Fly.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (13, 'Sleeper Stretch', 'Stretch', 'Rotator Cuff', '
1. Lie on the affected side. If you have no injury or pain, choose a side to start with. Your shoulder should be stacked underneath you.
2. Bring your elbow straight out from your shoulder and bend this arm, so your fingers are pointing toward the ceiling. This is the starting position.
3. Gently guide this arm toward the floor using the unaffected arm. Stop when you feel a stretch in the back of your affected shoulder.
4. Hold this position for up to 30 seconds.', 'https://post.healthline.com/wp-content/uploads/2019/12/400x400_Sleeper_Stretch_Sleeper_Stretch.gif');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (14, 'Fist Clench', 'Strengthen', 'Long Flexor Tendons', '
1. Sit at a table with your forearm resting on the table.
2. Hold a rolled-up towel or small ball in your hand.
3. Squeeze the towel in your hand and hold for 10 seconds.
4. Release and repeat on each side.', 'https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/topic_centers/Fitness-Exercise/400x400_5_Exercises_for_Tennis_Elbow_Rehab_Fist_Clench.gif?h=840');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (15, 'Wrist Extension', 'Strengthen', 'Wrist Extensors', '
1. Sit in a chair, holding a 2-pound dumbbell in your hand, with your palm facing down. Rest your elbow comfortably on your knee.
2. Keeping your palm facing down, extend your wrist by curling it toward your body. If this is too challenging, do the movement with no weight.
3. Return to the starting position and repeat on each side.
4. Try to isolate the movement to your wrist, keeping the rest of your arm still.', 'https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/topic_centers/Fitness-Exercise/400x400_5_Exercises_for_Tennis_Elbow_Rehab_Wrist_Extension.gif?h=840');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (16, 'Wrist Flexion', 'Strengthen', 'Wrist Flexors', '
1. Sit in a chair, holding a 2-pound dumbbell in your hand, with your palm facing up. Rest your elbow comfortably on your knee.
2. Keeping your palm facing up, flex your wrist by curling it toward your body.
3. Return to the starting position and repeat on each side.
4. Try to isolate the movement to your wrist, keeping the rest of your arm still.', 'https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/topic_centers/Fitness-Exercise/400x400_5_Exercises_for_Tennis_Elbow_Rehab_Wrist_Flexion.gif?h=840');
insert into [Exercise] ([Id], [Name], [Type], [Muscle], [Instructions], [VideoLocation]) values (17, 'Supination', 'Strengthen', 'Supinator Muscle', '
1. Sit in a chair, holding a 2-pound dumbbell vertically in your hand, with your elbow resting on your knee.
2. Let the weight of the dumbbell help rotate your arm outward, turning your palm up.
3. Rotate your hand back in the other direction until your palm is facing down in the starting position.
4. Repeat on each side trying to isolate the movement to your lower arm, keeping your upper arm and elbow still.', 'https://i0.wp.com/images-prod.healthline.com/hlcmsresource/images/topic_centers/Fitness-Exercise/400x400_5_Exercises_for_Tennis_Elbow_Rehab_Supination_with_A_Dumbbell.gif?h=840');
set identity_insert [Exercise] off

set identity_insert [UserProfile] on
--Admin
insert into UserProfile (Id, FirstName, LastName, Email, Password, CreateDateTime, ImageLocation, UserTypeId) values (1, 'Foo', 'Barington', 'foo@bar.com', 'Password', '2017-04-23 09:37:56', 'https://robohash.org/numquamutut.png?size=150x150&set=set1', 1);
--Patient
insert into UserProfile (Id, FirstName, LastName, Email, Password, CreateDateTime, ImageLocation, UserTypeId) values (2, 'Reina', 'Maruska', 'rmaruska0@google.com.brx', 'Password1', '2023-04-23 15:51:42', 'https://robohash.org/numquamutut.png?size=150x150&set=set1', 2);
insert into UserProfile (Id, FirstName, LastName, Email, Password, CreateDateTime, ImageLocation, UserTypeId) values (3, 'Cristabel', 'Van Der Weedenburg', 'cvanderweedenburg7@wikimedia.orgx', 'pass2', '2023-06-19 01:42:06', 'https://robohash.org/quidemearumtenetur.png?size=150x150&set=set1', 2);
insert into UserProfile (Id, FirstName, LastName, Email, Password, CreateDateTime, ImageLocation, UserTypeId) values (4, 'Tobi', 'Figiovanni', 'tfigiovanni5@baidu.comx', 'pass01', '2023-03-17 19:30:58', 'https://robohash.org/quiundedignissimos.png?size=150x150&set=set1', 2);
--Provider
insert into UserProfile (Id, FirstName, LastName, Email, Password, CreateDateTime, ImageLocation, UserTypeId) values (5, 'Arnold', 'Otton', 'aotton2@ow.lyx', 'Password!', '2018-04-22 10:34:23', 'https://robohash.org/molestiaemagnamet.png?size=150x150&set=set1', 3);
insert into UserProfile (Id, FirstName, LastName, Email, Password, CreateDateTime, ImageLocation, UserTypeId) values (6, 'Giuseppe', 'Teanby', 'gteanby6@craigslist.orgx', 'Password1!', '2019-08-29 12:47:18', 'https://robohash.org/hicnihilipsa.png?size=150x150&set=set1', 3);
set identity_insert [UserProfile] off

set identity_insert [PatientAssignment] on
insert into PatientAssignment (Id, PatientProfileId, ProviderProfileId, BeginDate, EndDate) values (1, 2, 5, '2023-04-23 08:47:18', '2023-12-22 08:47:18');
insert into PatientAssignment (Id, PatientProfileId, ProviderProfileId, BeginDate, EndDate) values (2, 3, 5, '2023-06-19 08:47:18', '2024-02-22 08:47:18');
insert into PatientAssignment (Id, PatientProfileId, ProviderProfileId, BeginDate, EndDate) values (3, 4, 6, '2023-03-18 08:47:18', '2023-09-22 08:47:18');
set identity_insert [PatientAssignment] off

set identity_insert [Note] on
insert into Note (Id, PatientProfileId, ProviderProfileId, Content, CreateDateTime) values (1, 2, 5, 'Patient had a dislocated patella due to football injury. Physician notes recommended quadricep stretching and strengthening to prevent patella tracking issues. Patient will focus on small stretching motions at home and when in person do some stretches but mostly strength training.', '2023-04-24 09:37:56');
insert into Note (Id, PatientProfileId, ProviderProfileId, Content, CreateDateTime) values (2, 3, 5, 'Patient visited complaining of shoulder stiffness, but no pain. Patient reports minimal physical activity and a sedentary lifestyle. Will attempt rudementary stretching and strengthening exercises to access current status.', '2023-06-20 06:50:42');
insert into Note (Id, PatientProfileId, ProviderProfileId, Content, CreateDateTime) values (3, 4, 6, 'Patient was referred by primary care physician due to history of tennis elbow. Patient is active when at home and has an active job in construction. Will focus on stretching exercises and recovery techniques.', '2023-03-19 08:19:03');
set identity_insert [Note] off

set identity_insert [Message] on
insert into Message (Id, FromId, ToId, Content, CreateDateTime) values (1, 5, 2, 'Hi Reina, as discussed we this will be a primary resource for your at home exercises where you can tell me how you feel about each exercise.', '2023-04-23 16:30:57');
set identity_insert [Message] off

set identity_insert [Regimen] on
insert into Regimen (Id, ProviderProfileId, Title, Description, CreateDateTime) values (1, 5, 'Patella Dislocation Recovery', 'Rehab plan meant for individuals who sustained a patella dislocations without further complications and no surgeries.', '2023-01-19 12:01:18');
insert into Regimen (Id, ProviderProfileId, Title, Description, CreateDateTime) values (2, 5, 'Shoulder Mobility Plan', 'Plan meant for individuals who sustained minor or no injuries and have over time due to sedentary lifestyle or lack of exercise have naturally become stiff.', '2022-03-27 10:43:06');
insert into Regimen (Id, ProviderProfileId, Title, Description, CreateDateTime) values (3, 5, 'Ankle Mobility Recovery', 'Rehab plan meant for individuals who sustained an ankle sprain or strain injury.', '2021-11-01 13:52:47');
insert into Regimen (Id, ProviderProfileId, Title, Description, CreateDateTime) values (4, 6, 'Tennis Elbow Recovery', 'Rehab plan meant for individuals who are active recreationally or at work and experience tennis elbow or similar conditions.', '2023-03-18 05:07:24');
set identity_insert [Regimen] off

set identity_insert [RegimenExercise] on
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (1, 1, 1);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (2, 1, 2);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (3, 1, 3);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (4, 1, 6);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (5, 1, 7);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (6, 1, 8);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (7, 2, 9);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (8, 2, 10);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (9, 2, 11);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (10, 2, 12);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (11, 2, 13);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (12, 3, 3);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (13, 3, 4);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (14, 3, 5);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (15, 3, 6);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (16, 3, 7);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (17, 4, 14);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (18, 4, 15);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (19, 4, 16);
insert into RegimenExercise (Id, RegimenId, ExerciseId) values (20, 4, 17);
set identity_insert [RegimenExercise] off

set identity_insert [RegimenAssignment] on
insert into RegimenAssignment (Id, RegimenId, PatientProfileId, AssignmentDate) values (1, 1, 2, '2023-12-22 08:47:18');
insert into RegimenAssignment (Id, RegimenId, PatientProfileId, AssignmentDate) values (2, 2, 3, '2024-02-22 08:47:18');
insert into RegimenAssignment (Id, RegimenId, PatientProfileId, AssignmentDate) values (3, 4, 4, '2023-09-22 08:47:18');
set identity_insert [RegimenAssignment] off

--still need comment, exercise reaction, and message
set identity_insert [ExerciseReaction] on
insert into ExerciseReaction (Id, RegimenExerciseId, ReactionId) values (1, 1, 1);
insert into ExerciseReaction (Id, RegimenExerciseId, ReactionId) values (2, 2, 2);
set identity_insert [ExerciseReaction] off


set identity_insert [Comment] on
insert into Comment (Id, UserId, RegimenExerciseId, Content, CreateDateTime) values (1, 2, 1, 'Feels great so far. Slightly challenging but not to the extent I am feeling pain.', '2023-04-24 09:18:31');
set identity_insert [Comment] off