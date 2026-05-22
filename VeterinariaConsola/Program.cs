using System.Net.Http.Json;
using System.Text.Json;

const string baseUrl = "http://localhost:5253";

using var http = new HttpClient { BaseAddress = new Uri(baseUrl) };

while (true)
{
	Console.WriteLine();
	Console.WriteLine("=== Veterinaria Consola ===");
	Console.WriteLine("1. Cargar dueño");
	Console.WriteLine("2. Cargar animal");
	Console.WriteLine("3. Cargar atencion");
	Console.WriteLine("0. Salir");
	Console.Write("Opcion: ");

	var opcion = Console.ReadLine();

	switch (opcion)
	{
		case "1":
			await CargarDuenioAsync(http);
			break;
		case "2":
			await CargarAnimalAsync(http);
			break;
		case "3":
			await CargarAtencionAsync(http);
			break;
		case "0":
			return;
		default:
			Console.WriteLine("Opcion invalida.");
			break;
	}
}

static async Task CargarDuenioAsync(HttpClient http)
{
	var dto = new DuenioCreateDto(
		ReadRequiredString("DNI"),
		ReadRequiredString("Nombre"),
		ReadRequiredString("Apellido")
	);

	var response = await http.PostAsJsonAsync("/duenios", dto);
	await MostrarRespuestaAsync(response);
}

static async Task CargarAnimalAsync(HttpClient http)
{
	var duenioId = ReadNullableInt("DuenioId (Enter si no tiene)");

	var dto = new AnimalCreateDto(
		ReadRequiredString("Nombre"),
		ReadRequiredInt("Edad"),
		ReadRequiredString("Sexo"),
		ReadRequiredInt("TipoId"),
		ReadRequiredInt("RazaId"),
		duenioId
	);

	var response = await http.PostAsJsonAsync("/animales", dto);
	await MostrarRespuestaAsync(response);
}

static async Task CargarAtencionAsync(HttpClient http)
{
	var fecha = ReadRequiredDateTime("Fecha (yyyy-MM-dd HH:mm)");

	var dto = new AtencionCreateDto(
		ReadRequiredString("Motivo"),
		ReadRequiredString("Tratamiento"),
		ReadRequiredString("Medicamentos"),
		fecha,
		ReadRequiredInt("AnimalId")
	);

	var response = await http.PostAsJsonAsync("/atenciones", dto);
	await MostrarRespuestaAsync(response);
}

static string ReadRequiredString(string label)
{
	while (true)
	{
		Console.Write($"{label}: ");
		var value = Console.ReadLine();

		if (!string.IsNullOrWhiteSpace(value))
			return value.Trim();

		Console.WriteLine("El valor es obligatorio.");
	}
}

static int ReadRequiredInt(string label)
{
	while (true)
	{
		Console.Write($"{label}: ");
		var input = Console.ReadLine();

		if (int.TryParse(input, out var value))
			return value;

		Console.WriteLine("Ingrese un numero valido.");
	}
}

static int? ReadNullableInt(string label)
{
	while (true)
	{
		Console.Write($"{label}: ");
		var input = Console.ReadLine();

		if (string.IsNullOrWhiteSpace(input))
			return null;

		if (int.TryParse(input, out var value))
			return value;

		Console.WriteLine("Ingrese un numero valido o deje vacio.");
	}
}

static DateTime ReadRequiredDateTime(string label)
{
	while (true)
	{
		Console.Write($"{label}: ");
		var input = Console.ReadLine();

		if (DateTime.TryParse(input, out var value))
			return value;

		Console.WriteLine("Ingrese una fecha valida.");
	}
}

static async Task MostrarRespuestaAsync(HttpResponseMessage response)
{
	var content = await response.Content.ReadAsStringAsync();

	Console.WriteLine($"Status: {(int)response.StatusCode} {response.ReasonPhrase}");

	if (string.IsNullOrWhiteSpace(content))
		return;

	try
	{
		using var doc = JsonDocument.Parse(content);
		var pretty = JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
		Console.WriteLine(pretty);
	}
	catch
	{
		Console.WriteLine(content);
	}
}

public record DuenioCreateDto(string Dni, string Nombre, string Apellido);
public record AnimalCreateDto(string Nombre, int Edad, string Sexo, int TipoId, int RazaId, int? DuenioId);
public record AtencionCreateDto(string Motivo, string Tratamiento, string Medicamentos, DateTime Fecha, int AnimalId);
