
--companies
INSERT [dbo].[Companies] ([Id], [Name], [Address], [Logo], [Email], [Website], [ADLoginYn], [MobileIronYn], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', N'Sentient', N'Goregaon(West)', NULL, N'sentient2008@gmail.com', NULL, 0, 0, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO

--roles
INSERT [dbo].[Roles] ([Id], [Name], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'1d0bf51c-3c8f-45b0-9a16-2732b251e88d', N'SiteAdmin', N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'947d14f6-bad9-49c6-8830-781fc9807963', N'DomainUser', N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'76cde1f7-950e-4826-8666-bea361d27772', N'Admin', N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[Roles] ([Id], [Name], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'633d5fd6-776f-4af1-8f76-d52adde38d79', N'User', N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO

--locations
INSERT [dbo].[Locations] ([Id], [Name], [Code], [Active], [CompanyId], [ParentId], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'3b988bb1-83fb-4fe9-8611-c6101940ffe3', N'Goregaon', N'GRN', 0, N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', NULL, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO

--Users
INSERT [dbo].[User] ([Id], [EmpCode], [UserName], [Name], [Password], [Mobile], [Email], [RoleId], [CompanyId], [LocationId], [DesignationId], [ReportingToUserId], [BusinessUnitId], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'9bad75f9-442e-447a-8385-4f7fe824fa90', N'VOV_SENTI_001', N'SentientAdmin', N'Sentient Admin',N'2220tbYOIVYfrreLt4BsDg==', N'9969875308', N'sentient2008@gmail.com', N'76cde1f7-950e-4826-8666-bea361d27772', N'CF1D1C0C-2F49-4941-BDA0-8F32232571B1', N'3b988bb1-83fb-4fe9-8611-c6101940ffe3', NULL, NULL, NULL, 1, N'f094a2c3-f6f2-4d1f-ad91-df17f820e1dd',  NULL, NULL)
GO

--BusinessSegments
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'aa49d145-2f39-4384-9471-1b093fadef00', N'Fashion industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'35d20eff-481d-4907-8a26-2de3e479c91f', N'Chemical industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'e26cd5b5-ab62-4442-b335-455623379623', N'Hotels industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'58bcb599-6860-43d4-8a0d-53739780eb69', N'Defense industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'c38dd507-ab7b-4dc6-8aca-5ea584832ce1', N'Construction industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'04e16ff4-7f76-4a21-9915-68c9cfd3f412', N'Education industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'583c79a8-ee74-46e4-a946-6ecc9bd715d0', N'Aerospace industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'02ac7728-f244-45f4-9c48-784befb32a5a', N'Entertainment industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'013ca040-e5e0-4563-b1a9-c3cdedc4241b', N'Automotive industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'7a396f20-50c1-4f46-9383-e05abf23d086', N'Computer industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO
INSERT [dbo].[BusinessSegments] ([Id], [Name], [Active], [CreatedById], [UpdatedById], [UpdatedOn]) VALUES (N'515655a2-4432-438a-b264-fa0399432e3c', N'Agricultural industry', 1, N'9bad75f9-442e-447a-8385-4f7fe824fa90',  NULL, NULL)
GO

--Business Units----
INSERT [dbo].[BusinessUnits] ([Id], [Name], [Code], [Active], [CompanyId], [ParentId], [CreatedById],  [UpdatedById], [UpdatedOn]) VALUES (N'89d82c7b-95ce-4e37-970f-516f30a63522', N'Sales', N'S', 1, N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', NULL, N'9bad75f9-442e-447a-8385-4f7fe824fa90', CAST(N'2023-08-08T15:46:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BusinessUnits] ([Id], [Name], [Code], [Active], [CompanyId], [ParentId], [CreatedById],  [UpdatedById], [UpdatedOn]) VALUES (N'13f6e635-5482-4f30-9336-c03e31c768de', N'Purchase', N'P', 1, N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', NULL, N'9bad75f9-442e-447a-8385-4f7fe824fa90', CAST(N'2023-08-08T15:46:00' AS SmallDateTime), NULL, NULL)
GO
--[Designations]--
INSERT [dbo].[Designations] ([Id], [Name], [Code], [Active], [CompanyId], [ParentId], [CreatedById],  [UpdatedById], [UpdatedOn]) VALUES (N'290b0d1e-d796-4d36-b0fd-7679634f8c33', N'Unit Head', N'UH', 1, N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', N'4870af54-c766-4e0a-a4ef-9f5fbc2201fc', N'9bad75f9-442e-447a-8385-4f7fe824fa90', CAST(N'2023-08-08T15:46:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[Designations] ([Id], [Name], [Code], [Active], [CompanyId], [ParentId], [CreatedById],  [UpdatedById], [UpdatedOn]) VALUES (N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', N'Relationship Officer', N'RO', 1, N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', NULL, N'9bad75f9-442e-447a-8385-4f7fe824fa90', CAST(N'2023-08-08T15:46:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[Designations] ([Id], [Name], [Code], [Active], [CompanyId], [ParentId], [CreatedById],  [UpdatedById], [UpdatedOn]) VALUES (N'4870af54-c766-4e0a-a4ef-9f5fbc2201fc', N'Team Lead', N'TL', 1, N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', N'cf1d1c0c-2f49-4941-bda0-8f32232571b1', N'9bad75f9-442e-447a-8385-4f7fe824fa90', CAST(N'2023-08-08T15:46:00' AS SmallDateTime), NULL, NULL)
GO
