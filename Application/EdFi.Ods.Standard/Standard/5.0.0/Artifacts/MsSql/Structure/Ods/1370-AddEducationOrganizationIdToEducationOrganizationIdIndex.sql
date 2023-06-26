CREATE NONCLUSTERED INDEX IX_EducationOrganizationIdToEducationOrganizationId ON auth.EducationOrganizationIdToEducationOrganizationId
(
	TargetEducationOrganizationId
) INCLUDE (SourceEducationOrganizationId)
GO