﻿namespace KarginScales.Service;

public interface IDialogService
{
    void ShowMessage(string? message, string title = "Сообщение");
}
