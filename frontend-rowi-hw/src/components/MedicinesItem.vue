<script>
import { HTTP } from "../api/http-common";
import { createToaster } from "@meforma/vue-toaster";
import { ref } from "vue";

const toaster = createToaster({
  position: "right",
});

export default {
  data() {
    return {
      id: 0,
      name: "",
      useMethod: "",
      effects: "",
      sideEffects: "",
    };
  },
  props: ["data", "popup"],
  emits: ["modalClose", "medicinesRefresh"],
  methods: {
    async save() {
      if (!this.id) {
        let response = await HTTP.post(`/api/medicines`, {
          name: this.name,
          useMethod: this.useMethod,
          effects: this.effects,
          sideEffects: this.sideEffects,
        });

        if (response.data) {
          this.$toast.success(`Created.`);
          this.popupCreateItem = 0;
        }
      } else {
        let response = await HTTP.put(`/api/medicines/${this.id}`, {
          Id: this.id,
          name: this.name,
          useMethod: this.useMethod,
          effects: this.effects,
          sideEffects: this.sideEffects,
        });

        //if (response.data) {
        let instance = this.$toast.success(`Updated.`);
        //setTimeout(instance.dismiss(), 100);
        //}
      }
      this.refreshClose();
    },
    refreshClose() {
      this.$emit("medicinesRefresh");
      this.$emit("modalClose");
    },
  },
  mounted() {
    this.popupCreateItem = this.popup ?? 0;
    this.isSingleItem = this.$route.params.id ?? false;
    const textInputOptions = ref({
      format: "MM/dd/yyyy",
    });
    this.id = this.data.id ?? 0;
    this.name = this.data.name ?? "";
    this.useMethod = this.data.useMethod ?? "";
    this.effects = this.data.effects ?? "";
    this.sideEffects = this.data.sideEffects ?? "";
  },
  setup() {},
};
</script>

<template>
  <div class="mt-10 sm:mt-0 py-2">
    <span
      class="text-lg float-right cursor-pointer ml-5"
      v-if="this.popup"
      @click="this.refreshClose()"
      >&times;</span
    >
    <div class="md:grid md:grid-cols-3 md:gap-6">
      <div class="md:col-span-1 m-auto">
        <div class="px-4 sm:px-0">
          <h3
            class="text-lg font-medium leading-6 text-gray-900"
            v-if="this.id"
          >
            <a
              :href="`/medicines/${this.id}`"
              class="text-blue-500 hover:text-blue-900"
              >Medicine #{{ this.id }}</a
            >
          </h3>
          <h3 class="text-lg font-medium leading-6 text-gray-900" v-else>
            <span>Create New Medicine</span>
          </h3>
          <p class="mt-1 text-sm text-gray-600">Fill the Information</p>
        </div>
      </div>
      <div class="mt-5 md:mt-0 md:col-span-2">
        <form method="POST">
          <div class="shadow overflow-hidden sm:rounded-md">
            <div class="px-4 py-5 bg-white sm:p-6">
              <div class="grid grid-cols-6 gap-6">
                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="name"
                    class="block text-sm font-medium text-gray-700"
                    >Medicine Name</label
                  >
                  <input
                    type="text"
                    name="name"
                    id="name"
                    v-model="this.name"
                    :class="
                      this.popup || this.isSingleItem
                        ? 'mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                        : ' pointer-events-none outline-none mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                    "
                  />
                </div>
                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="useMethod"
                    class="block text-sm font-medium text-gray-700"
                    >Use Method</label
                  >
                  <input
                    type="text"
                    name="useMethod"
                    id="useMethod"
                    v-model="this.useMethod"
                    :class="
                      this.isSingleItem || this.popup
                        ? 'mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                        : ' pointer-events-none outline-none mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                    "
                  />
                </div>
                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="effects"
                    class="block text-sm font-medium text-gray-700"
                    >Effects</label
                  >
                  <input
                    type="text"
                    name="effects"
                    id="effects"
                    v-model="this.effects"
                    :class="
                      this.isSingleItem || this.popup
                        ? 'mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                        : ' pointer-events-none outline-none mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                    "
                  />
                </div>
                <div class="col-span-6 sm:col-span-3">
                  <label
                    for="sideEffects"
                    class="block text-sm font-medium text-gray-700"
                    >Side Effects</label
                  >
                  <input
                    type="text"
                    name="sideEffects"
                    id="sideEffects"
                    v-model="this.sideEffects"
                    :class="
                      this.isSingleItem || this.popup
                        ? 'mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                        : ' pointer-events-none outline-none mt-1 focus:ring-indigo-500 focus:border-indigo-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md'
                    "
                  />
                </div>
              </div>
            </div>
            <div
              class="px-4 py-3 bg-gray-50 text-right sm:px-6"
              v-if="this.isSingleItem || this.popupCreateItem || this.popup"
            >
              <button
                type="submit"
                @click.prevent="save"
                class="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                Save
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
