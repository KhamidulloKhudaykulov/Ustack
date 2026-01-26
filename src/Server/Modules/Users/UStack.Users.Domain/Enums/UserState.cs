namespace UStack.Users.Domain.Enums;

public enum UserState
{
    Inactive =  0,    // account aktiv emas
    Active   =  1,      // account aktiv
    Locked   = -1,      // login muvaffaqiyatsizligi yoki admin tomonidan bloklangan
    Pending  =  5,     // email/identity verification kutilmoqda
    Archived = -5    // account o‘chirilgan yoki arxivlangan
}