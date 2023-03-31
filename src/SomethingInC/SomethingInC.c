#include "framework.h"
#include "SomethingInC.h"

#define MAX_LOADSTRING 100
#define VIEW_WIDTH 1280
#define VIEW_HEIGHT 720
#define DUDE_WIDTH 16
#define DUDE_HEIGHT 16

HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
POINT dudePos = { VIEW_WIDTH / 2 - DUDE_WIDTH/2, VIEW_HEIGHT / 2-DUDE_HEIGHT/2 };

ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_SOMETHINGINC, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_SOMETHINGINC));

	MSG msg;

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}

ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEXW wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_SOMETHINGINC));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	wcex.lpszMenuName = NULL;
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassExW(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // Store instance handle in our global variable

	HWND hWnd = CreateWindowW(
		szWindowClass,
		szTitle,
		WS_TILED | WS_SYSMENU,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		VIEW_WIDTH,
		VIEW_HEIGHT,
		NULL,
		NULL,
		hInstance,
		NULL);

	RECT rcClient = { 0 };
	RECT rcWindow = { 0 };
	GetClientRect(hWnd, &rcClient);
	GetWindowRect(hWnd, &rcWindow);
	OffsetRect(&rcWindow, -rcWindow.left, -rcWindow.top);
	SetWindowPos(hWnd, NULL, 0, 0, rcWindow.right + VIEW_WIDTH - rcClient.right, rcWindow.bottom + VIEW_HEIGHT - rcClient.bottom, SWP_NOMOVE);

	if (!hWnd)
	{
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

void UpdateBackground(HDC hdc)
{
	HBRUSH brush = CreateSolidBrush(RGB(0, 0, 0));
	RECT rcFill = { 0,0,VIEW_WIDTH,VIEW_HEIGHT };
	FillRect(hdc, &rcFill, brush);
	DeleteObject(brush);
}

void UpdateDude(HDC hdc)
{
	HBRUSH brush = CreateSolidBrush(RGB(255, 0, 0));
	RECT rcFill = { dudePos.x,dudePos.y,dudePos.x+DUDE_WIDTH,dudePos.y+DUDE_HEIGHT };
	FillRect(hdc, &rcFill, brush);
	DeleteObject(brush);
}

void UpdateFrame(HDC hdc)
{
	UpdateBackground(hdc);
	UpdateDude(hdc);
}

void HandleKey(HWND, int);

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hWnd, &ps);
		UpdateFrame(hdc);
		EndPaint(hWnd, &ps);
	}
	break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	case WM_KEYDOWN:
		HandleKey(hWnd, (int)wParam);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}

void HandleKey(HWND hWnd, int key)
{
	switch (key)
	{
	case VK_UP:
		dudePos.y -= DUDE_HEIGHT;
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	case VK_DOWN:
		dudePos.y += DUDE_HEIGHT;
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	case VK_LEFT:
		dudePos.x -= DUDE_WIDTH;
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	case VK_RIGHT:
		dudePos.x += DUDE_WIDTH;
		InvalidateRect(hWnd, NULL, FALSE);
		break;
	}
}
