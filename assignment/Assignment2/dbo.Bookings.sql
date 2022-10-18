CREATE TABLE [dbo].[Bookings] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME NOT NULL,
    [PatientId] INT            NOT NULL,
    [DoctorId]  INT            NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BookingPatient] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]),
    CONSTRAINT [FK_BookingDoctor] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_BookingPatient]
    ON [dbo].[Bookings]([PatientId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_BookingDoctor]
    ON [dbo].[Bookings]([DoctorId] ASC);

