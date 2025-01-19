//using UnityEngine;
//using UnityEngine.UI;  // Para usar Slider
//using TMPro;  // Para usar TextMeshProUGUI
//using System.Collections;  // Para usar Coroutines

//public class CorduraController : MonoBehaviour
//{
//    public Slider corduraSlider; // El Slider que representa la cordura
//    public TextMeshProUGUI porcentajeTexto; // El TextMeshProUGUI que muestra el porcentaje
//    public TextMeshProUGUI mensajeRecarga; // El TextMeshProUGUI para el mensaje de recarga de cordura
//    public GameObject panelApagado; // El Panel negro que simula el apagado
//    public float tiempoDesgaste = 1f;
//    public int desgastePorTiempo = 1;
//    public int desgastePorMision = 10;
//    public float tiempoMensaje = 2f; // Tiempo que el mensaje estará visible
//    public float tiempoDesvanecimiento = 1f; // Tiempo para el desvanecimiento
//    public float tiempoApagado = 0.5f; // Tiempo del efecto de apagado y encendido

//    private float tiempoActual = 0f;

//    void Start()
//    {
//        // Inicializa la cordura en 100 al empezar
//        corduraSlider.maxValue = 100;
//        corduraSlider.value = 100;

//        // Asegúrate de que el porcentaje se actualice al inicio
//        ActualizarPorcentaje();

//        // Asegúrate de que el mensaje de recarga esté oculto al principio
//        mensajeRecarga.gameObject.SetActive(false);

//        // Asegúrate de que el panel negro esté oculto al principio
//        panelApagado.SetActive(false);
//    }

//    void Update()
//    {
//        // Reduce la cordura gradualmente con el tiempo
//        tiempoActual += Time.deltaTime;
//        if (tiempoActual >= tiempoDesgaste)
//        {
//            ReducirCordura(desgastePorTiempo);
//            tiempoActual = 0f;
//        }
//    }

//    public void ReducirCordura(int cantidad)
//    {
//        corduraSlider.value -= cantidad;

//        // Asegúrate de que la cordura no sea menor que 0
//        if (corduraSlider.value <= 0)
//        {
//            corduraSlider.value = 0;
//            Debug.Log("El jugador ha perdido toda la cordura.");
//            // Aquí podrías agregar lógica para finalizar el juego
//        }

//        // Actualiza el porcentaje después de reducir la cordura
//        ActualizarPorcentaje();
//    }

//    public void RecargarCordura()
//    {
//        // Solo recarga la cordura después de que se haya desvanecido la pantalla negra
//        StartCoroutine(EfectoApagadoEncendido());
//    }

//    public void CompletarMision()
//    {
//        ReducirCordura(desgastePorMision);
//        Debug.Log("Misión completada, se redujo la cordura.");
//    }

//    private void ActualizarPorcentaje()
//    {
//        // Calcula el porcentaje de cordura
//        float porcentaje = (corduraSlider.value / corduraSlider.maxValue) * 100;

//        // Actualiza el texto para mostrar el porcentaje
//        if (porcentajeTexto != null)
//        {
//            porcentajeTexto.text = $"{porcentaje:F0}%"; // El F0 es para mostrar sin decimales
//        }
//    }

//    private IEnumerator MostrarMensajeRecargaConFade()
//    {
//        // Muestra el mensaje de recarga
//        mensajeRecarga.gameObject.SetActive(true);
//        mensajeRecarga.text = "La cordura se ha recargado.    Ahora me siento muchisimo mejor";

//        // Desvanecimiento inicial: aumentar la opacidad de 0 a 1
//        float tiempoTranscurrido = 0f;
//        while (tiempoTranscurrido < tiempoDesvanecimiento)
//        {
//            float alpha = Mathf.Lerp(0f, 1f, tiempoTranscurrido / tiempoDesvanecimiento);
//            mensajeRecarga.alpha = alpha;
//            tiempoTranscurrido += Time.deltaTime;
//            yield return null;
//        }

//        mensajeRecarga.alpha = 1f; // Asegura que esté completamente visible

//        // Espera el tiempo del mensaje antes de desvanecerlo
//        yield return new WaitForSeconds(tiempoMensaje);

//        // Desvanecimiento final: reducir la opacidad de 1 a 0
//        tiempoTranscurrido = 0f;
//        while (tiempoTranscurrido < tiempoDesvanecimiento)
//        {
//            float alpha = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoDesvanecimiento);
//            mensajeRecarga.alpha = alpha;
//            tiempoTranscurrido += Time.deltaTime;
//            yield return null;
//        }

//        mensajeRecarga.alpha = 0f; // Asegura que esté completamente oculto

//        // Desactiva el mensaje después de desvanecerse
//        mensajeRecarga.gameObject.SetActive(false);
//    }

//    private IEnumerator EfectoApagadoEncendido()
//    {
//        // Apagar la cámara: hacer que el panel negro cubra la pantalla
//        panelApagado.SetActive(true);

//        // Desvanecimiento del panel de apagado (opacidad de 0 a 1)
//        float tiempoTranscurrido = 0f;
//        while (tiempoTranscurrido < tiempoApagado)
//        {
//            float alpha = Mathf.Lerp(0f, 1f, tiempoTranscurrido / tiempoApagado);
//            panelApagado.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha);
//            tiempoTranscurrido += Time.deltaTime;
//            yield return null;
//        }

//        // Asegúrate de que el panel esté completamente oscuro
//        panelApagado.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);

//        // Espera un momento con el panel oscuro
//        yield return new WaitForSeconds(0.2f);

//        // Encender la cámara: hacer que el panel negro se desvanezca
//        tiempoTranscurrido = 0f;
//        while (tiempoTranscurrido < tiempoApagado)
//        {
//            float alpha = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoApagado);
//            panelApagado.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha);
//            tiempoTranscurrido += Time.deltaTime;
//            yield return null;
//        }

//        // Asegúrate de que el panel esté completamente invisible
//        panelApagado.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

//        // Desactiva el panel después de que se haya desvanecido
//        panelApagado.SetActive(false);

//        // Una vez que la pantalla se ha "encendido", recargar la cordura
//        corduraSlider.value = corduraSlider.maxValue;

//        // Muestra el mensaje de recarga con desvanecimiento
//        StartCoroutine(MostrarMensajeRecargaConFade());
//    }
//}


using UnityEngine;
using UnityEngine.UI;  // Para usar Slider
using TMPro;  // Para usar TextMeshProUGUI
using System.Collections;  // Para usar Coroutines

public class CorduraController : MonoBehaviour
{
    public Slider corduraSlider;
    public TextMeshProUGUI porcentajeTexto;
    public TextMeshProUGUI mensajeRecarga;
    public GameObject panelApagado;
    public float tiempoDesgaste = 1f;
    public int desgastePorTiempo = 1;
    public int desgastePorMision = 10;
    public float tiempoMensaje = 2f;
    public float tiempoDesvanecimiento = 1f;
    public float tiempoApagado = 0.5f;

    private float tiempoActual = 0f;

    // Referencia al script de movimiento del jugador
    public movement playerMovement;

    void Start()
    {
        corduraSlider.maxValue = 100;
        corduraSlider.value = 100;
        ActualizarPorcentaje();
        mensajeRecarga.gameObject.SetActive(false);
        panelApagado.SetActive(false);
    }

    void Update()
    {
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoDesgaste)
        {
            ReducirCordura(desgastePorTiempo);
            tiempoActual = 0f;
        }
    }

    public void ReducirCordura(int cantidad)
    {
        corduraSlider.value -= cantidad;

        if (corduraSlider.value <= 0)
        {
            corduraSlider.value = 0;
            Debug.Log("El jugador ha perdido toda la cordura.");
        }

        ActualizarPorcentaje();
    }

    public void RecargarCordura()
    {
        // Detener el movimiento del jugador al iniciar la recarga
        playerMovement.InmovilizarJugador();
        StartCoroutine(EfectoApagadoEncendido());
    }

    private IEnumerator EfectoApagadoEncendido()
    {
        panelApagado.SetActive(true);

        // Desvanecimiento del panel de apagado
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoApagado)
        {
            float alpha = Mathf.Lerp(0f, 1f, tiempoTranscurrido / tiempoApagado);
            panelApagado.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // Tiempo con pantalla apagada

        tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoApagado)
        {
            float alpha = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoApagado);
            panelApagado.GetComponent<Image>().color = new Color(0f, 0f, 0f, alpha);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        panelApagado.SetActive(false);

        // Recargar la cordura
        corduraSlider.value = corduraSlider.maxValue;

        // Reanudar el movimiento del jugador
        playerMovement.ReanudarMovimiento();

        // Mostrar mensaje de recarga
        StartCoroutine(MostrarMensajeRecargaConFade());
    }

    private void ActualizarPorcentaje()
    {
        float porcentaje = (corduraSlider.value / corduraSlider.maxValue) * 100;
        if (porcentajeTexto != null)
        {
            porcentajeTexto.text = $"{porcentaje:F0}%";
        }
    }

    private IEnumerator MostrarMensajeRecargaConFade()
    {
        mensajeRecarga.gameObject.SetActive(true);
        mensajeRecarga.text = "La cordura se ha recargado. Ahora me siento muchisimo mejor";

        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoDesvanecimiento)
        {
            float alpha = Mathf.Lerp(0f, 1f, tiempoTranscurrido / tiempoDesvanecimiento);
            mensajeRecarga.alpha = alpha;
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        mensajeRecarga.alpha = 1f;

        yield return new WaitForSeconds(tiempoMensaje);

        tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoDesvanecimiento)
        {
            float alpha = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoDesvanecimiento);
            mensajeRecarga.alpha = alpha;
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        mensajeRecarga.alpha = 0f;
        mensajeRecarga.gameObject.SetActive(false);
    }
}
