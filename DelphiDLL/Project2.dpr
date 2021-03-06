library Bridge;
uses
 Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ShellAPI;

{
type
   TMqlString = Record
    size:integer;
    buffer: PWideChar;
    res: integer;
   end;
}


procedure SendData(s: PWideChar); stdcall;
var
  str: String;
  CDS: TCopyDataStruct;
begin
  str:= WideCharToString(s);

  CDS.dwData := 1;
  CDS.cbData := length(str)+1;
  GetMem(CDS.lpData, CDS.cbData);
  try
    StrPCopy(CDS.lpData, AnsiString(str));
    SendMessage(FindWindow(nil, 'ForexCap'),
                  WM_COPYDATA, 0, Integer(@CDS));
  finally
    FreeMem(CDS.lpData, CDS.cbData);
  end;
end;


function GetData(): String; stdcall;
begin
  //Result:=comm;
end;

exports
SendData,
GetData;

begin
end.
